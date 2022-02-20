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

    private string formState = "login";

    public string GetUsernameFromInput()
    {
        usernameValue = usernameInput.GetComponent<InputField>();
        return usernameValue.text;
    }

    public string GetPasswordFromInput()
    {
        passwordValue = passwordInput.GetComponent<InputField>();
        return passwordValue.text;
    }

    public void GetUserData()
    {
        usernameErrorMessage.SetActive(false);
        passwordErrorMessage.SetActive(false);
        string username = GetUsernameFromInput();
        StartCoroutine(WebAPIAccess.GetPlayerData(username, AuthenticateUser));
    }

    public void InsertUserData()
    {
        usernameTakenMessage.SetActive(false);
        string username = GetUsernameFromInput();
        string password = GetPasswordFromInput();
        string salt = GenerateSalt();
        string saltedPassword = password + salt;
        string hashedPassword = SHA256Hash(saltedPassword);
        StartCoroutine(WebAPIAccess.InsertPlayerData(username, hashedPassword, salt, AuthenticateNewUser));
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
            if (userData.hash == hashed) {
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
        SceneManager.LoadSceneAsync("Menu Scene");
    }

    public void ConvertToSignUpForm()
    {
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
        formState = "login";
        usernameTakenMessage.SetActive(false);
        signupButton.SetActive(false);
        loginText.SetActive(false);
        loginButton.SetActive(true);
        signupText.SetActive(true);
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