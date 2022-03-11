using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEnd : MonoBehaviour
{
	bool isGameOver = false;
	public int score;
	public int balloons_popped;
	public float shot_accuracy;
	public int boosts_used;
	public int terrain_collisions;
	public string game_map;
	public string control;

	public GameObject yourScore;
	public GameObject balloonsPopped;
	public GameObject shotAccuracy;
	public GameObject boostsUsed;
	public GameObject newAchievements;

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
		terrain_collisions = PlayerPrefs.GetInt("terrain_collisions");
		game_map = sceneNameToMapName(Gameplay.scene);
		control = getControlMethod(Gameplay.keyboardMode);
		if (SceneManager.GetActiveScene().name == "End Scene")
		{
			SetEndStats();
			UploadData();
			UpdateAchievementProgress();
		}
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

	private void SetEndStats()
	{
		yourScore.GetComponent<Text>().text = "Your Score: " + score.ToString();
		balloonsPopped.GetComponent<Text>().text = "Balloons Popped: " + balloons_popped.ToString();
		shotAccuracy.GetComponent<Text>().text = "Shot Accuracy: " + (shot_accuracy * 100).ToString("0.0") + "%";
		boostsUsed.GetComponent<Text>().text = "Boosts Used: " + boosts_used.ToString();
		newAchievements.SetActive(true);
	}

	public void ReadUsername(string input)
	{
		StartCoroutine(WebAPIAccess.Upload(input, score, balloons_popped, shot_accuracy, boosts_used, game_map, control));
		SceneManager.LoadScene("Menu Scene");
	}

	public void UploadData()
	{
		StartCoroutine(WebAPIAccess.Upload(Player.username, score, balloons_popped, shot_accuracy, boosts_used, game_map, control));
	}

	public void UpdateAchievementProgress()
	{
		if (game_map == "Realistic")
		{
			if (!Achievements.CheckIfGotten(1))
			{
				StartCoroutine(WebAPIAccess.SetPlayerAchievement(Player.username, 1, 1));
				Achievements.GetAchievement(1);
			}
		}
		if (game_map == "Low-Poly")
		{
			if (!Achievements.CheckIfGotten(2))
			{
				StartCoroutine(WebAPIAccess.SetPlayerAchievement(Player.username, 2, 1));
				Achievements.GetAchievement(2);
			}
			if (Gameplay.retroCameraEnabled)
			{
				if (!Achievements.CheckIfGotten(31))
				{
					StartCoroutine(WebAPIAccess.SetPlayerAchievement(Player.username, 31, 1));
					Achievements.GetAchievement(31);
				}
			}
			if (Gameplay.daytime == "dawn")
			{
				if (!Achievements.CheckIfGotten(32))
				{
					StartCoroutine(WebAPIAccess.SetPlayerAchievement(Player.username, 32, 1));
					Achievements.GetAchievement(32);
				}
			}
			if (Gameplay.daytime == "day")
			{
				if (!Achievements.CheckIfGotten(33))
				{
					StartCoroutine(WebAPIAccess.SetPlayerAchievement(Player.username, 33, 1));
					Achievements.GetAchievement(33);
				}
			}
			if (Gameplay.daytime == "sunset")
			{
				if (!Achievements.CheckIfGotten(34))
				{
					StartCoroutine(WebAPIAccess.SetPlayerAchievement(Player.username, 34, 1));
					Achievements.GetAchievement(34);
				}
			}
			if (Gameplay.daytime == "dusk")
			{
				if (!Achievements.CheckIfGotten(35))
				{
					StartCoroutine(WebAPIAccess.SetPlayerAchievement(Player.username, 35, 1));
					Achievements.GetAchievement(35);
				}
			}
			if (!Achievements.CheckIfGotten(36) && Achievements.CheckIfGotten(32) && Achievements.CheckIfGotten(33) && Achievements.CheckIfGotten(34) && Achievements.CheckIfGotten(35))
			{
				StartCoroutine(WebAPIAccess.SetPlayerAchievement(Player.username, 36, 1));
				Achievements.GetAchievement(36);
			}
		}
		if (score >= 10)
		{
			if (!Achievements.CheckIfGotten(13))
			{
				StartCoroutine(WebAPIAccess.SetPlayerAchievement(Player.username, 13, 1));
				Achievements.GetAchievement(13);
			}
		}
		if (score >= 25)
		{
			if (!Achievements.CheckIfGotten(14))
			{
				StartCoroutine(WebAPIAccess.SetPlayerAchievement(Player.username, 14, 1));
				Achievements.GetAchievement(14);
			}
		}
		if (score >= 50)
		{
			if (!Achievements.CheckIfGotten(15))
			{
				StartCoroutine(WebAPIAccess.SetPlayerAchievement(Player.username, 15, 1));
				Achievements.GetAchievement(15);
			}
		}
		if (score >= 100)
		{
			if (!Achievements.CheckIfGotten(16))
			{
				StartCoroutine(WebAPIAccess.SetPlayerAchievement(Player.username, 16, 1));
				Achievements.GetAchievement(16);
			}
		}
		if (balloons_popped >= 10)
		{
			if (!Achievements.CheckIfGotten(17))
			{
				StartCoroutine(WebAPIAccess.SetPlayerAchievement(Player.username, 17, 1));
				Achievements.GetAchievement(17);
			}
		}
		if (balloons_popped >= 20)
		{
			if (!Achievements.CheckIfGotten(18))
			{
				StartCoroutine(WebAPIAccess.SetPlayerAchievement(Player.username, 18, 1));
				Achievements.GetAchievement(18);
			}
		}
		if (balloons_popped >= 30)
		{
			if (!Achievements.CheckIfGotten(19))
			{
				StartCoroutine(WebAPIAccess.SetPlayerAchievement(Player.username, 19, 1));
				Achievements.GetAchievement(19);
			}
		}
		if (balloons_popped >= 40)
		{
			if (!Achievements.CheckIfGotten(20))
			{
				StartCoroutine(WebAPIAccess.SetPlayerAchievement(Player.username, 20, 1));
				Achievements.GetAchievement(20);
			}
		}
		if (shot_accuracy >= .35)
		{
			if (!Achievements.CheckIfGotten(21))
			{
				StartCoroutine(WebAPIAccess.SetPlayerAchievement(Player.username, 21, 1));
				Achievements.GetAchievement(21);
			}
		}
		if (shot_accuracy >= .50)
		{
			if (!Achievements.CheckIfGotten(22))
			{
				StartCoroutine(WebAPIAccess.SetPlayerAchievement(Player.username, 22, 1));
				Achievements.GetAchievement(22);
			}
		}
		if (shot_accuracy >= .70)
		{
			if (!Achievements.CheckIfGotten(23))
			{
				StartCoroutine(WebAPIAccess.SetPlayerAchievement(Player.username, 23, 1));
				Achievements.GetAchievement(23);
			}
		}
		if (Achievements.CheckForValue(24) < 1)
		{
			StartCoroutine(WebAPIAccess.SetPlayerAchievement(Player.username, 24, Achievements.CheckForValue(24) + 1));
			Achievements.IncrementValue(24);
		}
		if (Achievements.CheckForValue(25) < 3)
		{
			StartCoroutine(WebAPIAccess.SetPlayerAchievement(Player.username, 25, Achievements.CheckForValue(25) + 1));
			Achievements.IncrementValue(25);
		}
		if (Achievements.CheckForValue(26) < 5)
		{
			StartCoroutine(WebAPIAccess.SetPlayerAchievement(Player.username, 26, Achievements.CheckForValue(26) + 1));
			Achievements.IncrementValue(26);
		}
		if (Achievements.CheckForValue(27) < 10)
		{
			StartCoroutine(WebAPIAccess.SetPlayerAchievement(Player.username, 27, Achievements.CheckForValue(27) + 1));
			Achievements.IncrementValue(27);
		}
		if (score == 0)
		{
			if (!Achievements.CheckIfGotten(28))
			{
				StartCoroutine(WebAPIAccess.SetPlayerAchievement(Player.username, 28, 1));
				Achievements.GetAchievement(28);
			}
		}
		if (terrain_collisions == 0) {
			if (!Achievements.CheckIfGotten(29))
			{
				StartCoroutine(WebAPIAccess.SetPlayerAchievement(Player.username, 29, 1));
			}
		}
	}
}
