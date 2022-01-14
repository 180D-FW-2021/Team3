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
	public static string scene = "LowPolyScene";

	public static int musicVolume = 100;
	public static int engineVolume = 100;
	public static bool minimapEnabled = true;
	public static bool retroCameraEnabled = false;

	public static float gameTime = 180f;
	
	public static float minThrottle = 0f;
	public static float maxThrottle = 2f;
	public static float acceleration = .004f;
	public static float deceleration = .001f;
	public static float minSpeed = .16f;
	public static float maxSpeed = 2f;

	public static float engineVolumeNormalizerValue = .1f;
	public static float hgrThrottleIncrement = .1f;
	public static float keyboardThrottleIncrement = .01f; 
	public static float gravityInfluenceMultiplier = .015f;
	public static float imuCentripetalMultiplier = .011f;
	public static float imuTurnMultiplier = .022f;
	public static float shotCooldown = 350f;
	public static float boostDuration = 1000f;
	public static float boostSpeed = 8f;

	public static float cloudMovementSpeed = 35f;
	public static float balloonMovementSpeed = 50f;

	public static float balloon1BobbingMultiplier = .3f;
	public static float balloon2BobbingMultiplier = .5f;
	public static float balloon3BobbingMultiplier = .7f;
	public static float balloon5BobbingMultiplier = .85f;
	public static float balloon10BobbingMultiplier = 1f;

	public static float balloon1WindMultiplier = 0f;
	public static float balloon2WindMultiplier = .125f;
	public static float balloon3WindMultiplier = .25f;
	public static float balloon5WindMultiplier = .5f;
	public static float balloon10WindMultiplier = 1f;

	public static float balloonIconHeight = 750f;

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

	public static void adjustScale(string scaleType, bool increase)
	{
		switch (scaleType)
		{
			case "music":
				if (increase && musicVolume < 200)
				{
					musicVolume += 10;
				}
				if (!increase && musicVolume > 0)
				{
					musicVolume -= 10;
				}
				break;
			case "engine":
				if (increase && engineVolume < 200)
				{
					engineVolume += 10;
				}
				if (!increase && engineVolume > 0)
				{
					engineVolume -= 10;
				}
				break;
			default:
				break;
		}
	}

	public static void setScale(string scaleType, int value)
	{
		switch (scaleType)
		{
			case "music":
				musicVolume = value;
				break;
			case "engine":
				engineVolume = value;
				break;
			default:
				break;
		}
	}

	public static void toggle(string toggleType)
	{
		switch (toggleType)
		{
			case "minimap":
				minimapEnabled = !minimapEnabled;
				break;
			case "retroCamera":
				retroCameraEnabled = !retroCameraEnabled;
				break;
			default:
				break;
		}
	}

	public static void setToggle(string toggleType, bool value)
	{
		switch (toggleType)
		{
			case "minimap":
				minimapEnabled = value;
				break;
			case "retroCamera":
				retroCameraEnabled = value;
				break;
			default:
				break;
		}
	}
}
