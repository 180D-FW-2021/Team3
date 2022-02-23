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

	RaycastHit[] hits;

	public GameObject hitParticleSystem;

	// Start is called before the first frame update
	void Start()
	{
		ShooterInstance = ShooterObject.GetComponent<Shooter>();
		hits = new RaycastHit[1];
	}

	// Update is called once per frame
	void Update()
	{
		lastPosition = transform.position;
		transform.position += transform.forward * speed * Time.deltaTime;
		if (gameObject.activeInHierarchy)
		{
			CheckHit();

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
					if (target.isShootable == 1)
					{
						target.TakeDamage(damage);
					}
					switch (hits[0].transform.name)
					{
						case "Balloon1(Clone)":
							ShooterInstance.score += 1;
							ShooterInstance.shotsHit += 1;
							Instantiate(hitParticleSystem, hits[0].point, Quaternion.LookRotation(hits[0].normal));
							if (!Achievements.CheckIfGotten(3))
							{
								StartCoroutine(WebAPIAccess.SetPlayerAchievement(Player.username, 3, 1));
								Achievements.GetAchievement(3);
							}
							break;
						case "Balloon2(Clone)":
							ShooterInstance.score += 2;
							ShooterInstance.shotsHit += 1;
							Instantiate(hitParticleSystem, hits[0].point, Quaternion.LookRotation(hits[0].normal));
							if (!Achievements.CheckIfGotten(4))
							{
								StartCoroutine(WebAPIAccess.SetPlayerAchievement(Player.username, 4, 1));
								Achievements.GetAchievement(4);
							}
							break;
						case "Balloon3(Clone)":
							ShooterInstance.score += 3;
							ShooterInstance.shotsHit += 1;
							Instantiate(hitParticleSystem, hits[0].point, Quaternion.LookRotation(hits[0].normal));
							if (!Achievements.CheckIfGotten(5))
							{
								StartCoroutine(WebAPIAccess.SetPlayerAchievement(Player.username, 5, 1));
								Achievements.GetAchievement(5);
							}
							break;
						case "Balloon5(Clone)":
							ShooterInstance.score += 5;
							ShooterInstance.shotsHit += 1;
							Instantiate(hitParticleSystem, hits[0].point, Quaternion.LookRotation(hits[0].normal));
							if (!Achievements.CheckIfGotten(6))
							{
								StartCoroutine(WebAPIAccess.SetPlayerAchievement(Player.username, 6, 1));
								Achievements.GetAchievement(6);
							}
							break;
						case "Balloon10(Clone)":
							ShooterInstance.score += 10;
							ShooterInstance.shotsHit += 1;
							Instantiate(hitParticleSystem, hits[0].point, Quaternion.LookRotation(hits[0].normal));
							if (!Achievements.CheckIfGotten(7))
							{
								StartCoroutine(WebAPIAccess.SetPlayerAchievement(Player.username, 7, 1));
								Achievements.GetAchievement(7);
							}
							break;
						case "BalloonGold":
							ShooterInstance.shotsHit += 1;
							Instantiate(hitParticleSystem, hits[0].point, Quaternion.LookRotation(hits[0].normal));
							break;
						case "Heart":
							ShooterInstance.score += 9;
							ShooterInstance.shotsHit += 1;
							Instantiate(hitParticleSystem, hits[0].point, Quaternion.LookRotation(hits[0].normal));
							if (!Achievements.CheckIfGotten(37))
							{
								StartCoroutine(WebAPIAccess.SetPlayerAchievement(Player.username, 37, 1));
								Achievements.GetAchievement(37);
							}
							break;
						default:
							break;
					}
					Destroy(gameObject);
				}
				else if (hits[0].transform.name != "Plane"){
					Instantiate(hitParticleSystem, hits[0].point, Quaternion.LookRotation(hits[0].normal));
					Destroy(gameObject);
				}
			}
		}
	}
}
