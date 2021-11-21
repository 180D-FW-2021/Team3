using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public GameObject ShooterObject;
	public Shooter ShooterInstance;
	Vector3 lastPosition;
	public Camera fpsCam;
	public float speed = 500f;
	public float damage = 100f;
	public int score = 0;

	private Camera mainCamera;
	public Vector2 widthThreshold;
	public Vector2 heightThreshold;

	RaycastHit[] hits;
	// Start is called before the first frame update
	void Start()
	{
		ShooterInstance = ShooterObject.GetComponent<Shooter>();
		hits = new RaycastHit[1];
		Destroy(gameObject, 20f);
	}

	// Update is called once per frame
	void Update()
	{
		lastPosition = transform.position;
		transform.position += transform.forward * speed * Time.deltaTime;
		if (gameObject.active)
		{
			CheckHit();
		}
	}

	void CheckHit()
	{
		Ray ray = new Ray(lastPosition, fpsCam.transform.forward);
		float dist = Vector3.Distance(lastPosition, transform.position);
		if (Physics.RaycastNonAlloc(ray, hits, dist) > 0)
		{
			Target target = hits[0].transform.GetComponent<Target>();
			if (target != null)
			{
				target.TakeDamage(damage);
				Debug.Log(target.transform.name);
				//this.shotsHit++;
				switch (hits[0].transform.name)
				{
					case "Balloon1(Clone)":
						ShooterInstance.score += 1;
						break;
					case "Balloon2(Clone)":
						ShooterInstance.score += 2;
						break;
					case "Balloon3(Clone)":
						ShooterInstance.score += 3;
						break;
					case "Balloon5(Clone)":
						ShooterInstance.score += 5;
						break;
					case "Balloon10(Clone)":
						ShooterInstance.score += 10;
						break;
					default:
						break;
				}
			}
			Destroy(gameObject);
		}
	}
}
