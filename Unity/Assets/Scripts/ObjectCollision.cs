using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectCollision : MonoBehaviour
{
	public GameObject PlaneObject;
	public PlaneControl PlaneInstance;
	public GameObject ShooterObject;
	public Shooter ShooterInstance;
	public GameObject hitParticleSystem;
	public GameObject TimerObject;
	public CountdownTimer TimerInstance;
	public Color originalColor;
	public float damage = 100f;
	public Vector3 spawnLocation;
	public int timePenalty;

	private float lastTerrainCollision = 0f;

	void Start()
	{
		ShooterInstance = ShooterObject.GetComponent<Shooter>();
		PlaneInstance = PlaneObject.GetComponent<PlaneControl>();
		TimerInstance = TimerObject.GetComponent<CountdownTimer>();
		originalColor = TimerObject.GetComponent<Text>().color;
	}

	// Balloon Collision
	private void OnTriggerEnter(Collider collision)
	{
		Target target = collision.transform.GetComponent<Target>();
		target.TakeDamage(damage);
		switch (collision.transform.name)
		{
			case "Balloon1(Clone)":
				ShooterInstance.score += 1;
				ShooterInstance.shotsHit += 1;
				ShooterInstance.shotsTaken += 1;
				Instantiate(hitParticleSystem, collision.transform.position, Quaternion.LookRotation(collision.transform.forward));
				break;
			case "Balloon2(Clone)":
				ShooterInstance.score += 2;
				ShooterInstance.shotsHit += 1;
				ShooterInstance.shotsTaken += 1;
				Instantiate(hitParticleSystem, collision.transform.position, Quaternion.LookRotation(collision.transform.forward));
				break;
			case "Balloon3(Clone)":
				ShooterInstance.score += 3;
				ShooterInstance.shotsHit += 1;
				ShooterInstance.shotsTaken += 1;
				Instantiate(hitParticleSystem, collision.transform.position, Quaternion.LookRotation(collision.transform.forward));
				break;
			case "Balloon5(Clone)":
				ShooterInstance.score += 5;
				ShooterInstance.shotsHit += 1;
				ShooterInstance.shotsTaken += 1;
				Instantiate(hitParticleSystem, collision.transform.position, Quaternion.LookRotation(collision.transform.forward));
				break;
			case "Balloon10(Clone)":
				ShooterInstance.score += 10;
				ShooterInstance.shotsHit += 1;
				ShooterInstance.shotsTaken += 1;
				Instantiate(hitParticleSystem, collision.transform.position, Quaternion.LookRotation(collision.transform.forward));
				break;
			case "BalloonGold":
				ShooterInstance.score += 15;
				ShooterInstance.shotsHit += 1;
				ShooterInstance.shotsTaken += 1;
				TimerInstance.timeLeft += 15;
				Instantiate(hitParticleSystem, collision.transform.position, Quaternion.LookRotation(collision.transform.forward));
				break;
			case "Heart":
				ShooterInstance.score += 9;
				ShooterInstance.shotsHit += 1;
				ShooterInstance.shotsTaken += 1;
				Instantiate(hitParticleSystem, collision.transform.position, Quaternion.LookRotation(collision.transform.forward));
				break;
			default:
				break;
		}
	}

	// Terrain Collision
	private void OnCollisionEnter(Collision collision)
	{
		if (Time.time - lastTerrainCollision > .1)
		{
			lastTerrainCollision = Time.time;
			modifyTrailRenderer("Plane/LeftWingTrail", 0);
			modifyTrailRenderer("Plane/RightWingTrail", 0);
			PlaneInstance.transform.position = spawnLocation;
			StartCoroutine(EnableTrail());
			PlaneObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
			PlaneObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
			TimerInstance.timeLeft -= timePenalty;
			StartCoroutine(ChangeColor());
		}
	}

	private void modifyTrailRenderer(string trailName, int trailTime)
	{
		GameObject wingTrail = GameObject.Find(trailName);
		if (wingTrail)
		{
			wingTrail.GetComponent<TrailRenderer>().time = trailTime;
		}
	}

	private IEnumerator EnableTrail()
	{
		yield return null;
		yield return null;
		modifyTrailRenderer("Plane/LeftWingTrail", 8);
		modifyTrailRenderer("Plane/RightWingTrail", 8);
	}

	private IEnumerator ChangeColor()
	{
		TimerObject.GetComponent<Text>().color = Color.red;
		yield return new WaitForSeconds(0.5f);
		TimerObject.GetComponent<Text>().color = originalColor;
		yield return new WaitForSeconds(0.2f);
		TimerObject.GetComponent<Text>().color = Color.red;
		yield return new WaitForSeconds(0.5f);
		TimerObject.GetComponent<Text>().color = originalColor;
		yield return new WaitForSeconds(0.2f);
		TimerObject.GetComponent<Text>().color = Color.red;
		yield return new WaitForSeconds(1f);
		TimerObject.GetComponent<Text>().color = originalColor;
	}
}
