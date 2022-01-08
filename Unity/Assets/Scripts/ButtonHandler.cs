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

	public void startGame()
	{
		sceneName = GetSceneName(sceneToggle.value);
		Gameplay.scene = sceneName;
		StartCoroutine(loadScene(sceneName));
		//Gameplay.startGame();
		//SceneManager.LoadSceneAsync("Main Scene");
	}

	IEnumerator loadScene(string scene)
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
		loadingScreen.SetActive(true);

		while (!operation.isDone)
		{
			float progress = Mathf.Clamp01(operation.progress / .9f);
			planeLocation.anchoredPosition = new Vector2(progress * 1808 - 1308, planeLocation.anchoredPosition.y);
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
		//UnityEditor.EditorApplication.isPlaying = false;
	}

	public void goToMainMenu()
	{
		SceneManager.LoadScene("Menu Scene");
	}

	public void resumeGame()
	{
		Gameplay.resumeGame();
	}
}
