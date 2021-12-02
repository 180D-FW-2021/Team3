using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
	public float timeLeft = 180;
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
		float minutes = Mathf.FloorToInt(time / 60);
		float seconds = Mathf.FloorToInt(time % 60);
		displayTimer.text = string.Format("{0:0}:{1:00}", minutes, seconds) + " left";
	}
}
