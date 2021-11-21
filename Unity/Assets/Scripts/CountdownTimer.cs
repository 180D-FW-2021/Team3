﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
	public GameObject WebAPIObject;
	public WebAPIAccess WebAPIAccessInstance;
	public float timeLeft = 180;
	private bool updated = false;
	[SerializeField] Text displayTimer;

	// Start is called before the first frame update
	void Start()
	{
		WebAPIAccessInstance = WebAPIObject.GetComponent<WebAPIAccess>();
	}

	// Update is called once per frame
	void Update()
	{
		if (timeLeft > 0)
		{
			timeLeft -= Time.deltaTime;
		}
		else
		{
			timeLeft = 0;
			if (!updated)
			{
				StartCoroutine(WebAPIAccessInstance.Upload());
				updated = true;
			}
		}
		DisplayTime(timeLeft);
	}

	void DisplayTime(float time)
	{
		float minutes = Mathf.FloorToInt(time / 60);
		float seconds = Mathf.FloorToInt(time % 60);
		displayTimer.text = minutes.ToString() + ":" + seconds.ToString() + "  left";
	}
}
