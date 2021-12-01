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
	public float damage = 100f;


	void Start()
	{
		ShooterInstance = ShooterObject.GetComponent<Shooter>();
		PlaneInstance = PlaneObject.GetComponent<PlaneControl>();
		TimerInstance = TimerObject.GetComponent<CountdownTimer>();
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
				Instantiate(hitParticleSystem, collision.transform.position, Quaternion.LookRotation(collision.transform.forward));
				break;
			case "Balloon2(Clone)":
				ShooterInstance.score += 2;
				ShooterInstance.shotsHit += 1;
				Instantiate(hitParticleSystem, collision.transform.position, Quaternion.LookRotation(collision.transform.forward));
				break;
			case "Balloon3(Clone)":
				ShooterInstance.score += 3;
				ShooterInstance.shotsHit += 1;
				Instantiate(hitParticleSystem, collision.transform.position, Quaternion.LookRotation(collision.transform.forward));
				break;
			case "Balloon5(Clone)":
				ShooterInstance.score += 5;
				ShooterInstance.shotsHit += 1;
				Instantiate(hitParticleSystem, collision.transform.position, Quaternion.LookRotation(collision.transform.forward));
				break;
			case "Balloon10(Clone)":
				ShooterInstance.score += 10;
				ShooterInstance.shotsHit += 1;
				Instantiate(hitParticleSystem, collision.transform.position, Quaternion.LookRotation(collision.transform.forward));
				break;
			default:
				break;
		}
	}

	// Terrain Collision
	private void OnCollisionEnter(Collision collision)
	{
		PlaneInstance.transform.position = new Vector3(0f, 100f, 0f);
		PlaneObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
		PlaneObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		TimerInstance.timeLeft -= 20;
		StartCoroutine(ChangeColor());
	}

	private IEnumerator ChangeColor()
	{
		TimerObject.GetComponent<Text>().color = Color.red;
		yield return new WaitForSeconds(0.5f);
		TimerObject.GetComponent<Text>().color = Color.black;
		yield return new WaitForSeconds(0.2f);
		TimerObject.GetComponent<Text>().color = Color.red;
		yield return new WaitForSeconds(0.5f);
		TimerObject.GetComponent<Text>().color = Color.black;
		yield return new WaitForSeconds(0.2f);
		TimerObject.GetComponent<Text>().color = Color.red;
		yield return new WaitForSeconds(1f);
		TimerObject.GetComponent<Text>().color = Color.black;
	}
}
