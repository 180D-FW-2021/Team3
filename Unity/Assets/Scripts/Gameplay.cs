using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Gameplay
{
	public static bool isPaused = false;

	public static void pauseGame()
	{
		if (!isPaused)
		{
			Debug.Log("pause");
			SceneManager.LoadScene("Menu Scene");
			isPaused = !isPaused;
		}
	}

	public static void resumeGame()
	{
		if (isPaused)
		{
			Debug.Log("resume");
			SceneManager.LoadScene("Main Scene");
			isPaused = !isPaused;
		}
	}

	public static void quitGame()
	{
		Application.Quit();
	}
}
