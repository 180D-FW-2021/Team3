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
				if (!Achievements.CheckIfGotten(3))
				{
					StartCoroutine(WebAPIAccess.SetPlayerAchievement(Player.username, 3, 1));
					Achievements.GetAchievement(3);
				}
				break;
			case "Balloon2(Clone)":
				ShooterInstance.score += 2;
				ShooterInstance.shotsHit += 1;
				ShooterInstance.shotsTaken += 1;
				Instantiate(hitParticleSystem, collision.transform.position, Quaternion.LookRotation(collision.transform.forward));
				if (!Achievements.CheckIfGotten(4))
				{
					StartCoroutine(WebAPIAccess.SetPlayerAchievement(Player.username, 4, 1));
					Achievements.GetAchievement(4);
				}
				break;
			case "Balloon3(Clone)":
				ShooterInstance.score += 3;
				ShooterInstance.shotsHit += 1;
				ShooterInstance.shotsTaken += 1;
				Instantiate(hitParticleSystem, collision.transform.position, Quaternion.LookRotation(collision.transform.forward));
				if (!Achievements.CheckIfGotten(5))
				{
					StartCoroutine(WebAPIAccess.SetPlayerAchievement(Player.username, 5, 1));
					Achievements.GetAchievement(5);
				}
				break;
			case "Balloon5(Clone)":
				ShooterInstance.score += 5;
				ShooterInstance.shotsHit += 1;
				ShooterInstance.shotsTaken += 1;
				Instantiate(hitParticleSystem, collision.transform.position, Quaternion.LookRotation(collision.transform.forward));
				if (!Achievements.CheckIfGotten(6))
				{
					StartCoroutine(WebAPIAccess.SetPlayerAchievement(Player.username, 6, 1));
					Achievements.GetAchievement(6);
				}
				break;
			case "Balloon10(Clone)":
				ShooterInstance.score += 10;
				ShooterInstance.shotsHit += 1;
				ShooterInstance.shotsTaken += 1;
				Instantiate(hitParticleSystem, collision.transform.position, Quaternion.LookRotation(collision.transform.forward));
				if (!Achievements.CheckIfGotten(7))
				{
					StartCoroutine(WebAPIAccess.SetPlayerAchievement(Player.username, 7, 1));
					Achievements.GetAchievement(7);
				}
				break;
			case "BalloonGold":
				ShooterInstance.score += 15;
				ShooterInstance.shotsHit += 1;
				ShooterInstance.shotsTaken += 1;
				TimerInstance.timeLeft += 15;
				Instantiate(hitParticleSystem, collision.transform.position, Quaternion.LookRotation(collision.transform.forward));
				if (!Achievements.CheckIfGotten(8))
				{
					StartCoroutine(WebAPIAccess.SetPlayerAchievement(Player.username, 8, 1));
					Achievements.GetAchievement(8);
				}
				break;
			case "Heart":
				ShooterInstance.score += 9;
				ShooterInstance.shotsHit += 1;
				ShooterInstance.shotsTaken += 1;
				Instantiate(hitParticleSystem, collision.transform.position, Quaternion.LookRotation(collision.transform.forward));
				if (!Achievements.CheckIfGotten(37))
				{
					StartCoroutine(WebAPIAccess.SetPlayerAchievement(Player.username, 37, 1));
					Achievements.GetAchievement(37);
				}
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
			ShooterInstance.terrainCollisions += 1;
			if (!Achievements.CheckIfGotten(9))
			{
				StartCoroutine(WebAPIAccess.SetPlayerAchievement(Player.username, 9, 1));
				Achievements.GetAchievement(9);
			}
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
