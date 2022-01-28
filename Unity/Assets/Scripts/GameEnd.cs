using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
	bool isGameOver = false;
	public int score;
	public int balloons_popped;
	public float shot_accuracy;
	public int boosts_used;
	public string game_map;
	public string control;

	public void EndGame()
	{
		if (isGameOver == false)
		{
			isGameOver = true;
			SceneManager.LoadScene("End Scene");

		}
	}

	void OnEnable()
	{
		score = PlayerPrefs.GetInt("score");
		balloons_popped = PlayerPrefs.GetInt("balloons_popped");
		shot_accuracy = PlayerPrefs.GetFloat("shot_accuracy");
		boosts_used = PlayerPrefs.GetInt("boosts_used");
		game_map = sceneNameToMapName(Gameplay.scene);
		control = getControlMethod(Gameplay.keyboardMode);
	}

	private string sceneNameToMapName(string sceneName)
	{
		switch(sceneName)
		{
			case "Main Scene":
				return "Realistic";
			case "LowPolyScene":
				return "Low-Poly";
			default:
				return "N/A";
		}
	}

	private string getControlMethod(bool keyboardMode) {
		if (keyboardMode) {
			return "keyboard";
		}
		else {
			return "plane";
		}
	}

	public void ReadUsername(string input)
	{
		StartCoroutine(WebAPIAccess.Upload(input, score, balloons_popped, shot_accuracy, boosts_used, game_map, control));
		SceneManager.LoadScene("Menu Scene");
	}
}
