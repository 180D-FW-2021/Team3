using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Gameplay 
{
	[SerializeField] static GameObject MainMenuContainer;
	[SerializeField] static GameObject OptionsMenu;
	public static bool isPaused = false;
	public static bool keyboardMode = false;
	public static bool gameStarted = false;
	public static string scene = "Main Scene";

	public static int musicVolume = 100;
	public static bool retroCameraEnabled = false;

	public static void startGame() // not used
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
		if (isPaused && gameStarted)
		{
			Debug.Log("Game Resume");
			Time.timeScale = 1f;
			isPaused = !isPaused;
		}
	}

	public static void quitGame()
	{
		Application.Quit();
		//UnityEditor.EditorApplication.isPlaying = false;
	}

	public static void restartGame()
	{
		SceneManager.LoadSceneAsync("Menu Scene");
	}

	public static void loadSettings()
	{
		if (getCurrentScene() == "Menu Scene")
		{
			SceneManager.LoadSceneAsync("Settings Menu");
		}
	}

	public static void enableKeyboard()
	{
		keyboardMode = true;
	}

	public static string getCurrentScene()
	{
		return SceneManager.GetActiveScene().name;
	}

	public static void incrementMusicVolume()
	{
		if (musicVolume < 200)
		{
			musicVolume += 10;
		}
	}

	public static void decrementMusicVolume()
	{
		if (musicVolume > 0)
		{
			musicVolume -= 10;
		}
	}

	public static void toggleRetroCamera()
	{
		retroCameraEnabled = !retroCameraEnabled;
	}
}
