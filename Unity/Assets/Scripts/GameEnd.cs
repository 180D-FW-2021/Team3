using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;




public class GameEnd : MonoBehaviour
{
    bool isGameOver = false;
    public string username;
    public void EndGame()
    {
        if(isGameOver == false)
        {
        isGameOver = true;
        SceneManager.LoadScene("End Scene");

        }
    }

    public void ReadUsername(string input)
    {
        username = input;
        Debug.Log(username);
        SceneManager.LoadScene("Menu Scene");
    }
}
