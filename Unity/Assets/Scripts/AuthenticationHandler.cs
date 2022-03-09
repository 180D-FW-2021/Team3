using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AuthenticationHandler : MonoBehaviour
{
    public GameObject usernameInput;
    private InputField usernameValue;
    public GameObject usernameErrorMessage;
    public GameObject usernameTakenMessage;
    public GameObject passwordInput;
    private InputField passwordValue;
    public GameObject passwordErrorMessage;
    public GameObject loginButton;
    public GameObject signupButton;
    public GameObject loginText;
    public GameObject signupText;

    private const string glyphs = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    public string formState = "login";

    private AudioSource[] audioSources; // 0:default

    public void Start()
    {
        audioSources = gameObject.GetComponents<AudioSource>();
    }

    public string GetUsernameFromInput()
    {
        usernameValue = usernameInput.GetComponent<InputField>();
        if (usernameValue.text == "")
        {
            return "guest";
        }
        return usernameValue.text;
    }

    public string GetPasswordFromInput()
    {
        passwordValue = passwordInput.GetComponent<InputField>();
        return passwordValue.text;
    }

    public void GetUserData()
    {
        audioSources[0].Play();
        usernameErrorMessage.SetActive(false);
        passwordErrorMessage.SetActive(false);
        string username = GetUsernameFromInput();
        StartCoroutine(WebAPIAccess.GetPlayerData(username, AuthenticateUser));
    }

    public void InsertUserData()
    {
        audioSources[0].Play();
        usernameTakenMessage.SetActive(false);
        string username = GetUsernameFromInput();
        string password = GetPasswordFromInput();
        string salt = GenerateSalt();
        string saltedPassword = password + salt;
        string hashedPassword = SHA256Hash(saltedPassword);
        StartCoroutine(WebAPIAccess.InsertPlayerData(username, hashedPassword, salt, AuthenticateNewUser));
        StartCoroutine(WebAPIAccess.InsertNewPlayerAchievements(username));
    }

    public void AuthenticateUser(string data) {
        if (data.Length == 2) //invalid username
        {
            usernameErrorMessage.SetActive(true);
        }
        else //valid username
        {
            string clippedData = data.Substring(1, data.Length - 2); //remove item from array
            UserDataEntry userData = JsonUtility.FromJson<UserDataEntry>(clippedData);
            string unhashedPassword = GetPasswordFromInput();
            string saltedPassword = unhashedPassword + userData.salt;
            string hashed = SHA256Hash(saltedPassword);
            if (userData.hash == hashed || userData.username == "guest") {
                //correct password
                LoginUser(userData.username);
            }
            else {
                //incorrect password
                passwordErrorMessage.SetActive(true);
            }
        }
    }

    public void AuthenticateNewUser(InsertStatus insertStatus) {
        if (insertStatus.dbResponse.Length < 1) {
            usernameTakenMessage.SetActive(true);
        }
        else {
            Player.newUser = true;
            LoginUser(insertStatus.username);
        }
    }

    public string GenerateSalt() {
        string salt = "";
        for (int index = 0; index < Gameplay.saltLength; index++)
        {
            salt += glyphs[Random.Range(0, glyphs.Length - 1)];
        }
        return salt;
    }

    public string SHA256Hash(string data) {
        SHA256 mySHA256 = SHA256.Create();
        byte[] hashArray = mySHA256.ComputeHash(Encoding.UTF8.GetBytes(data));
        string hashed = bytesToHex(hashArray);
        return hashed;
    }

    public string bytesToHex(byte[] byteArray)
    {
        StringBuilder hexString = new StringBuilder(byteArray.Length * 2);
        foreach (byte b in byteArray)
        {
            hexString.AppendFormat("{0:x2}", b);
        }
        return hexString.ToString();
    }

    public void LoginUser(string username)
    {
        Player.username = username;
        getPlayerPreferences();
        getPlayerAchievements();
        SceneManager.LoadSceneAsync("Menu Scene");
    }

    public void ConvertToSignUpForm()
    {
        audioSources[0].Play();
        formState = "signup";
        usernameErrorMessage.SetActive(false);
        passwordErrorMessage.SetActive(false);
        loginButton.SetActive(false);
        signupText.SetActive(false);
        signupButton.SetActive(true);
        loginText.SetActive(true);
    }

    public void ConvertToLoginForm()
    {
        audioSources[0].Play();
        formState = "login";
        usernameTakenMessage.SetActive(false);
        signupButton.SetActive(false);
        loginText.SetActive(false);
        loginButton.SetActive(true);
        signupText.SetActive(true);
    }

    public void getPlayerPreferences()
    {
        StartCoroutine(WebAPIAccess.GetPlayerData(Player.username, setPlayerPreferences));
    }

    public void setPlayerPreferences(string data)
    {
        if (data.Length == 2) //invalid username
        {
            Debug.Log("How did we get here?");
        }
        else //valid username
        {
            string clippedData = data.Substring(1, data.Length - 2); //remove item from array
            PreferenceProfile userData = JsonUtility.FromJson<PreferenceProfile>(clippedData);
            Gameplay.setScale("music", userData.music_volume);
            Gameplay.setScale("engine", userData.engine_volume);
            Gameplay.setToggle("minimap", userData.intToBool(userData.minimap));
            Gameplay.setToggle("retroCamera", userData.intToBool(userData.retro_camera));
            Gameplay.setTimeOfDay(userData.daytime);
        }
    }

    public void getPlayerAchievements()
    {
        StartCoroutine(WebAPIAccess.GetPlayerAchievements(Player.username, setPlayerAchievements));
    }

    public void setPlayerAchievements(string data)
    {
        string clippedData = data.Substring(1, data.Length - 2); 
        DBFormat userData = JsonUtility.FromJson<DBFormat>(clippedData);
        Achievements.SetLocalAchievements(userData);
        Debug.Log(data);
    }
}

[System.Serializable]
public class UserDataEntry
{
    public string username;
    public string hash;
    public string salt;
}

[System.Serializable]
public class InsertStatus
{
    public string dbResponse;
    public string username;
}

[System.Serializable]
public class PreferenceProfile
{
    public int music_volume;
    public int engine_volume;
    public int minimap;
    public int retro_camera;
    public string daytime;

    public bool intToBool(int value)
    {
        return (value == 1);
    }
}