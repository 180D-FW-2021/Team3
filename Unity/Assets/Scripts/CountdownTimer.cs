using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
	public float timeLeft = Gameplay.gameTime;
	private bool updated = false;
	[SerializeField] Text displayTimer;

	// Update is called once per frame
	void Update()
	{
		if (timeLeft > 0)
		{
			timeLeft -= Time.deltaTime;
		}
		else
		{
			FindObjectOfType<GameEnd>().EndGame();
			timeLeft = 0;
			if (!updated)
			{
				updated = true;
			}
			
		}
		DisplayTime(timeLeft);
	}

	void DisplayTime(float time)
	{
		float minutes = time > 0 ? Mathf.FloorToInt(time / 60) : 0f;
		float seconds = time > 0 ? Mathf.FloorToInt(time % 60) : 0f;
		displayTimer.text = string.Format("{0:0}:{1:00}", minutes, seconds) + " Left";
	}
}
