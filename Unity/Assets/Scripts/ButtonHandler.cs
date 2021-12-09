using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
	// Start is called before the first frame update
	public void startGame()
	{
		SceneManager.LoadSceneAsync("Main Scene");
	}

	public void quitGame()
	{
		Application.Quit();
		UnityEditor.EditorApplication.isPlaying = false;
	}

	public void goToMainMenu()
	{
		SceneManager.LoadScene("Menu Scene");
	}
}
