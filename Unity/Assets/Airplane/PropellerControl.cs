using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerControl : MonoBehaviour
{
	[SerializeField] float propMultiplier = 1000f;
    GameObject plane;
    PlaneControl planeControl;

    // Start is called before the first frame update
    void Start()
    {
        plane = GameObject.FindWithTag("Plane");
        planeControl = plane.GetComponent<PlaneControl>();
    }

	// Update is called once per frame
	void Update()
	{
		transform.localRotation *= Quaternion.AngleAxis(propMultiplier * planeControl.throttle * Time.deltaTime, Vector3.up);
	}
}
