using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMotion : MonoBehaviour
{
    private bool rotatedToWind = false;
    private float cloudMovementSpeed;
    private Vector3 heightNormalizer;
    
    void Start()
    {
        cloudMovementSpeed = Gameplay.cloudMovementSpeed;
        heightNormalizer = new Vector3(0f, .1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!rotatedToWind)
        {
            rotatedToWind = true;
            this.transform.Rotate(new Vector3(0,Mathf.Atan(SpawnBalloons.wind.x / SpawnBalloons.wind.z) * 180 / Mathf.PI + 180,0));
        }
        if (!Gameplay.isPaused)
        {
            transform.localPosition = this.transform.localPosition + Vector3.Scale(SpawnBalloons.wind, heightNormalizer) * cloudMovementSpeed * Time.deltaTime;
        }
    }
}
