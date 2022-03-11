using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Diagnostics; 

public class ButtonHandler : MonoBehaviour
{
	public Slider sceneToggle;
	public static string sceneName;
	public GameObject loadingScreen;
	public Image loadingPlane;
	public Text loadingText;
	public Text tipText;
	private RectTransform planeLocation;
	private AudioSource[] audioSources; // 0:default, 1:start/pause, 2:controller
	private string[] tipList;
	private static bool handGes = true;
	Process hgr = new Process();

	// Start is called before the first frame update
	public void Start()
	{
		if(handGes == true)
		{
			hgr.StartInfo.FileName = Environment.CurrentDirectory + @"/hgr";
			hgr.Start();
		}
		handGes = false;
		audioSources = gameObject.GetComponents<AudioSource>();
		try
		{
			sceneToggle = GameObject.Find("SceneSelector").GetComponent<Slider>();
			sceneToggle.value = GetSceneIndex(Gameplay.scene);
		}
		catch (Exception e)
		{
			UnityEngine.Debug.Log(e);
		}
		planeLocation = loadingPlane.GetComponent<RectTransform>();


	}

	public string GetTip(string scene)
	{
		switch(scene)
		{
			case "Main Scene":
				tipList = new string[Gameplay.generalTips.Length + Gameplay.realisticTips.Length];
				Gameplay.generalTips.CopyTo(tipList, 0);
				Gameplay.realisticTips.CopyTo(tipList, Gameplay.generalTips.Length);
				break;
			case "LowPolyScene":
				tipList = new string[Gameplay.generalTips.Length + Gameplay.lowPolyTips.Length];
				Gameplay.generalTips.CopyTo(tipList, 0);
				Gameplay.lowPolyTips.CopyTo(tipList, Gameplay.generalTips.Length);
				break;
			default:
				tipList = new string[Gameplay.generalTips.Length];
				tipList = Gameplay.generalTips;
				break;
		}
		return tipList[UnityEngine.Random.Range(0, tipList.Length)];
	}

	public string GetSceneName(float index)
	{
		switch(index)
		{
			case 0:
				return "Main Scene";
			case 1:
				return "LowPolyScene";
			default:
				return "Main Scene";
		}
	}

	public float GetSceneIndex(string name)
	{
		switch(name)
		{
			case "Main Scene":
				return 0f;
			case "LowPolyScene":
				return 1f;
			default:
				return 0f;
		}
	}

	public void startGame()
	{
		audioSources[1].Play();
		sceneName = GetSceneName(sceneToggle.value);
		Gameplay.scene = sceneName;
		StartCoroutine(loadScene(sceneName));
	}

	public void restartGame()
	{
		audioSources[1].Play();
		StartCoroutine(loadScene(Gameplay.scene));
	}

	IEnumerator loadScene(string scene)
	{
		yield return new WaitForSeconds(1);
		AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
		loadingScreen.SetActive(true);
		tipText.text = GetTip(scene);

		while (!operation.isDone)
		{
			float progress = Mathf.Clamp01(operation.progress / .9f);
			planeLocation.anchoredPosition = new Vector2(progress * 1200 - 700, planeLocation.anchoredPosition.y);
			if (progress >= 0 && progress <= .25)
			{
				loadingText.text = "Refueling...";
			}
			else if (progress > .25 && progress <= .5)
			{
				loadingText.text = "Checking Instruments...";
			}
			else if (progress > .5 && progress <= .75)
			{
				loadingText.text = "Getting Clearance...";
			}
			else if (progress > .75 && progress < 1)
			{
				loadingText.text = "Taxiing...";
			}
			else if (progress == 1)
			{
				loadingText.text = "Taking Off...";
			}
			yield return null;
		}
	}

	public void openLeaderboard()
	{
		audioSources[0].Play();
		if (!Achievements.CheckIfGotten(30))
		{
			StartCoroutine(WebAPIAccess.SetPlayerAchievement(Player.username, 30, 1));
			Achievements.GetAchievement(30);
		}
		Application.OpenURL("https://aeroplay.online");
	}

	public void logout()
	{
		audioSources[0].Play();
		Gameplay.loadAuthentication();
	}

	public void quitGame(String optional = null)
	{
		audioSources[0].Play();
		Application.Quit();
	}

	public void rageQuit()
	{
		if (!Achievements.CheckIfGotten(38))
		{
			StartCoroutine(WebAPIAccess.SetPlayerAchievement(Player.username, 38, 1, quitGame));
			Achievements.GetAchievement(38); //pointless since we're quitting
		}
		else
		{
			quitGame();
		}
	}

	public void goToSettings()
	{
		audioSources[0].Play();
		Gameplay.loadSettings();
	}

	public void goToMainMenu()
	{
		audioSources[0].Play();
		Gameplay.resumeGame();
		SceneManager.LoadScene("Menu Scene");
	}

	public void toggleMap()
	{
		audioSources[0].Play();
		sceneToggle.value = Mathf.Abs(sceneToggle.value - 1);
	}

	public void setMap(string mapName)
	{
		audioSources[0].Play();
		switch (mapName)
		{
			case "realistic":
				sceneToggle.value = 0;
				break;
			case "lowPoly":
				sceneToggle.value = 1;
				break;
			default:
				sceneToggle.value = 1;
				break;
		}
	}

	public void pauseGame()
	{
		audioSources[1].Play();
		Gameplay.pauseGame();
	}

	public void resumeGame()
	{
		audioSources[0].Play();
		Gameplay.resumeGame();
	}

	public void enableKeyboard()
	{
		Gameplay.enableKeyboard();
	}
	
	public void controllerConnected() {
		audioSources[2].Play();
	}
}
