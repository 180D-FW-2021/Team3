using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] float bias = 0.971f; 
	PlaneControl planeControl;

	// Start is called before the first frame update
	void Start()
	{
		planeControl = GetComponent<PlaneControl>();
	}

	// Update is called once per frame
	void Update()
	{
		if (!Gameplay.isPaused)
		{
			var planePos = GameObject.Find("Plane");
			Vector3 moveCamTo = planePos.transform.position - planePos.transform.forward * 20.0f + Vector3.up * 10.0f;
			Camera.main.transform.position = Camera.main.transform.position*bias +  moveCamTo*(1.0f-bias);
			Camera.main.transform.LookAt(planePos.transform.position + planePos.transform.forward * 25.0f);
		}
	}
}
