using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooter : MonoBehaviour {
    public float damage = 100f;
    public float range = Mathf.Infinity;
    public int layerMask = 0;

    public int shotsTaken = 0;
    public int shotsHit = 0;
    public int score = 0;

    public Camera fpsCam;

    // Allows access to shoot function 
    private static Shooter ShooterInstance;
    [SerializeField] Text displayScore;
	public static Shooter instance
	{
		get
		{
			if(ShooterInstance==null)
			{
				ShooterInstance = FindObjectOfType(typeof(Shooter)) as Shooter;
			}
			return ShooterInstance;
		}
	}

    // Start is called before the first frame update
    void Start() {
        layerMask = 1 << 9;
        layerMask = ~layerMask;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Fire1")) {
            Shoot();
        }
        displayScore.text = "Score: " + score.ToString();
    }

    public void Shoot() {
        RaycastHit hit;
        this.shotsTaken++;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, layerMask)) {
            //Debug.Log(hit.transform.name); 

            Target target = hit.transform.GetComponent<Target>();
            if (target != null) {
                target.TakeDamage(damage);
                Debug.Log(target.transform.name);
                this.shotsHit++;
                switch(hit.transform.name) {
                    case "Balloon1(Clone)":
                        this.score += 1;
                        break;
                    case "Balloon2(Clone)":
                        this.score += 2;
                        break;
                    case "Balloon3(Clone)":
                        this.score += 3;
                        break;
                    case "Balloon5(Clone)":
                        this.score += 5;
                        break;
                    case "Balloon10(Clone)":
                        this.score += 10;
                        break;
                    default:
                        break;
                }
                Debug.Log(score.ToString() + " " + GetShotAccuracy().ToString());
            }
        }
    }

    public double GetShotAccuracy() {
        return (double)this.shotsHit / this.shotsTaken;
    }
}
