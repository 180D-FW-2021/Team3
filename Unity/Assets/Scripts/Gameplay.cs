using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Gameplay 
{
	[SerializeField] static GameObject MainMenuContainer;
	[SerializeField] static GameObject OptionsMenu;
	public static bool isPaused = false;
	public static bool keyboardMode = true;
	public static string scene = "Main Scene";

	public static void startGame()
	{
		scene = ButtonHandler.sceneName;
		SceneManager.LoadSceneAsync(scene);
	}

	public static void pauseGame()
	{
		if (!isPaused)
		{
			Debug.Log("Game Paused");
			Time.timeScale = 0f;
			isPaused = !isPaused;
		}
	}

	public static void resumeGame()
	{
		if (isPaused)
		{
			Debug.Log("Game Resume");
			Time.timeScale = 1f;
			isPaused = !isPaused;
		}
	}

	public static void quitGame()
	{
		Application.Quit();
		UnityEditor.EditorApplication.isPlaying = false;
	}

	public static void restartGame()
	{
		SceneManager.LoadSceneAsync("Menu Scene");
	}

	public static void loadOptions()
	{
		Scene currentScene = SceneManager.GetActiveScene();
		if (currentScene.name == "Menu Scene")
		{
			// TBD load objects
		}
	}

	public static void enableKeyboard()
	{
		keyboardMode = true;
	}
}
