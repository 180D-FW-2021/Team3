using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMotion : MonoBehaviour
{
    private bool rotatedToWind;
    // Start is called before the first frame update
    void Start()
    {
        rotatedToWind = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!rotatedToWind)
        {
            rotatedToWind = true;
            this.transform.Rotate(new Vector3(0,Mathf.Atan(SpawnBalloons.wind.x / SpawnBalloons.wind.z) * 180 / Mathf.PI + 180,0));
        }
        transform.localPosition = this.transform.localPosition + SpawnBalloons.wind * 1.5f;
    }
}
