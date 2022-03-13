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
	public static string daytime = "sunset";

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
	public static float imuCentripetalMultiplier = .005F;
	public static float imuTurnMultiplier = .02F;
	public static float imuTiltMultiplier = -.75f;
	public static float shotCooldown = 350f;
	public static float boostDuration = 1000f;
	public static float boostSpeed = 8f;
	public static float boostCooldown = 10000f;

	public static float cloudMovementSpeed = 35f;
	public static float balloonMovementSpeed = 50f;
	public static float hotAirBalloonMovementSpeed = 30f;

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

	public static float balloonIconHeight = 900f;

	public static string[] generalTips = {"Tip: Blue Balloons Are Worth 10 Points!", "Tip: Smaller Balloons Move Much Faster!", "Tip: Smaller Balloons Are Worth More Points!", "Tip: Boosts Ignore Drag! (WOW)", "Tip: Gotta Go Fast!", "Tip: Throttle Down To Gain Control!", "Tip: figure it out yourself lol", "Tip: Watch The Time!", "Tip: Remember To Breathe!", "Tip: You Can Collide With Balloons For Points!", "Tip: Avoid Terrain Collisions!", "Tip: Drink Plenty Of Water!", "Tip: Remember To Compensate For Wind!", "Tip: Some Balloons Are Rarer Than Others!", "Tip: Boost To Get To Higher Altitudes!", "Tip: Check The Online Leaderboards!", "Tip: Accelerate More By Pitching Downwards!", "Tip: Pitching Upwards Reduces Speed!", "Tip: Managing Throttle Is Key To Controlling Speed!"};
	public static string[] realisticTips = {"Tip: Balloon Locations Are Shown On The Minimap!"};
	public static string[] lowPolyTips = {"Tip: Gold Balloons Give Time And Points!", "Tip: Gold Balloons Cannot Be Shot Down!", "Tip: Gold Balloons Are Shown On The Minimap!", "Tip: Heart Of The Mountain", "Tip: Try The Retro Camera!", "Tip: Watch Out For Moving Obstacles!", "Tip: Some Gold Balloons Require A Specific Attack Angle!", "Tip: Look For The Beacons!", "Tip: Wind Turbines Point Against The Wind Direction!"};

	public static int saltLength = 32;

	public static bool newAchievements = false;

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

	public static void loadAuthentication()
	{
		SceneManager.LoadSceneAsync("Authentication");
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

	public static void adjustTimeOfDay(bool increase) // maybe change this to use enum
	{
		int currentTimeOfDayIndex = getIndexFromTimeOfDay(daytime);
		string newDaytime = increase ? getTimeOfDayFromIndex(currentTimeOfDayIndex + 1) : getTimeOfDayFromIndex(currentTimeOfDayIndex - 1);
		daytime = newDaytime;
	}

	public static void setTimeOfDay(string timeOfDay)
	{
		daytime = timeOfDay;
	}

	private static int getIndexFromTimeOfDay(string timeOfDay)
	{
		switch (timeOfDay)
		{
			case "dawn":
				return 0;
			case "day":
				return 1;
			case "sunset":
				return 2;
			case "dusk":
				return 3;
			default:
				return 2;
		}
	}

	private static string getTimeOfDayFromIndex(int index)
	{
		switch (index)
		{
			case -1:
				return "dusk";
			case 0:
				return "dawn";
			case 1:
				return "day";
			case 2:
				return "sunset";
			case 3:
				return "dusk";
			case 4:
				return "dawn";
			default:
				return "sunset";
		}
	}
}
