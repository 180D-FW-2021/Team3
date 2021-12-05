using UnityEngine;
using UnityEngine.SceneManagement;




public class GameEnd : MonoBehaviour
{
	bool isGameOver = false;
	public int score;
	public int balloons_popped;
	public float shot_accuracy;
	public int boosts_used;

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
	}

	public void ReadUsername(string input)
	{
		StartCoroutine(WebAPIAccess.Upload(input, score, balloons_popped, shot_accuracy, boosts_used));
		SceneManager.LoadScene("Menu Scene");
	}
}
