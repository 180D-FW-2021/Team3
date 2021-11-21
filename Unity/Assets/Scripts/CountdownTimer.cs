using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
	public float timeLeft = 180;
    [SerializeField] Text displayTimer;

	// Start is called before the first frame update
	void Start()
	{

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
