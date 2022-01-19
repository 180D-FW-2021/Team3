using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
	PlaneControl planeControl;
	private Vector3 velocity = Vector3.zero;
	public Vector3 offset = new Vector3(0,5,-20);
	public float cameraSmoothTime = .125f;
	public float cameraRotationSpeed = 25f;
	public float cameraTiltBias = 50f;

	void Start()
	{
		planeControl = GetComponent<PlaneControl>();
	}

	void Update()
	{
		if (!Gameplay.isPaused)
		{
			var planePosition = GameObject.Find("Plane");
			Vector3 targetPosition = planePosition.transform.TransformPoint(offset);
			Quaternion targetRotation = Quaternion.LookRotation(planePosition.transform.position - transform.position + planePosition.transform.forward * cameraTiltBias);
			Camera.main.transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, cameraSmoothTime);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, cameraRotationSpeed * Time.deltaTime);
		}
	}
}
