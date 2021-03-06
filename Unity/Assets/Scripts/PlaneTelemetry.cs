using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneTelemetry : MonoBehaviour
{
    [SerializeField] Text throttle, speed, altitude;
    PlaneControl planeControl;

    // Start is called before the first frame update
    void Start()
    {
        planeControl = GetComponent<PlaneControl>();
    }

    void SetStats() 
    {
        var planePosY = GameObject.Find("Plane").transform.position.y;
        throttle.text = (Math.Round(planeControl.throttle*50, 0)).ToString() + "%";
        speed.text = (Math.Round((planeControl.airSpeed + planeControl.airSpeedFromBoost) * 125, 0)).ToString()  +  " kph";
        altitude.text = (Math.Round(planePosY - 47.7, 1)).ToString() + "m";
    }

    // Update is called once per frame
    void Update()
    {
        SetStats();
    }

}
