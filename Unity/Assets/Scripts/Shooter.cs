using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooter : MonoBehaviour
{
	public int layerMask = 0;
	public int shotsTaken = 0;
	public int shotsHit = 0;
	public int score = 0;
	public GameObject bullet;

	// Allows access to shoot function 
	private static Shooter ShooterInstance;
	[SerializeField] Text displayScore;
	public static Shooter instance
	{
		get
		{
			if (ShooterInstance == null)
			{
				ShooterInstance = FindObjectOfType(typeof(Shooter)) as Shooter;
			}
			return ShooterInstance;
		}
	}

	// Start is called before the first frame update
	void Start()
	{
		layerMask = 1 << 9;
		layerMask = ~layerMask;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			Shoot();
		}
		displayScore.text = "Score: " + score.ToString();
	}

	public void Shoot()
	{
		this.shotsTaken++;
		var bulletInstance = (GameObject)Instantiate(bullet, transform.position, transform.rotation);
        Destroy(bulletInstance, 10f);
	}

	public double GetShotAccuracy()
	{
		return (double)this.shotsHit / this.shotsTaken;
	}
}
