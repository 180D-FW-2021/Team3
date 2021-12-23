using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEndScore : MonoBehaviour
{

	public int score;
	[SerializeField] Text display_score;
	// Start is called before the first frame update
	void OnEnable()
	{
		score = PlayerPrefs.GetInt("score");
	}

	// Update is called once per frame
	void Update()
	{
		display_score.text = "Your Score: " + score.ToString();
	}

}
