using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
	public Slider sceneToggle;
	public static string sceneName;
	// Start is called before the first frame update
	public void Start()
	{
		sceneToggle = GameObject.Find("SceneSelector").GetComponent<Slider>();
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
		loadScene(sceneName);
		//Gameplay.startGame();
		//SceneManager.LoadSceneAsync("Main Scene");
	}

	public void loadScene(string scene)
	{
		SceneManager.LoadSceneAsync(scene);
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
