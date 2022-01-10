using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
	public Slider sceneToggle;
	public static string sceneName;
	public GameObject loadingScreen;
	public Image loadingPlane;
	public Text loadingText;
	private RectTransform planeLocation;

	// Start is called before the first frame update
	public void Start()
	{
		sceneToggle = GameObject.Find("SceneSelector").GetComponent<Slider>();
		sceneToggle.value = GetSceneIndex(Gameplay.scene);
		planeLocation = loadingPlane.GetComponent<RectTransform>();
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
		sceneName = GetSceneName(sceneToggle.value);
		Gameplay.scene = sceneName;
		StartCoroutine(loadScene(sceneName));
	}

	public void restartGame()
	{
		StartCoroutine(loadScene(Gameplay.scene));
	}

	IEnumerator loadScene(string scene)
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
		loadingScreen.SetActive(true);

		while (!operation.isDone)
		{
			float progress = Mathf.Clamp01(operation.progress / .9f);
			planeLocation.anchoredPosition = new Vector2(progress * 1200 - 700, planeLocation.anchoredPosition.y);
			if (progress > 0 && progress <= .25)
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
		Application.OpenURL("https://aeroplay.online");
	}

	public void quitGame()
	{
		Application.Quit();
	}

	public void goToSettings()
	{
		Gameplay.loadSettings();
	}

	public void goToMainMenu()
	{
		SceneManager.LoadScene("Menu Scene");
	}

	public void toggleMap()
	{
		sceneToggle.value = Mathf.Abs(sceneToggle.value - 1);
	}

	public void setMap(string mapName)
	{
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
		Gameplay.pauseGame();
	}

	public void resumeGame()
	{
		Gameplay.resumeGame();
	}

	public void enableKeyboard()
	{
		Gameplay.enableKeyboard();
	}
}
