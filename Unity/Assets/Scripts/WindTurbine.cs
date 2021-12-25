using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTurbine : MonoBehaviour
{
    int rotateToWind;
    float randomMultiplier;
    GameObject turbine;

    // Start is called before the first frame update
    void Start()
    {
        rotateToWind = 0;
        randomMultiplier = Random.Range(480f, 520f);
        turbine = this.gameObject.transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (rotateToWind == 0)
        {
            rotateToWind = 1;
            this.transform.Rotate(new Vector3(0,Mathf.Atan(SpawnBalloons.wind.x / SpawnBalloons.wind.z) * 180 / Mathf.PI + 180,0));
        }
        turbine.transform.localRotation *= Quaternion.AngleAxis(randomMultiplier * SpawnBalloons.wind.magnitude * Time.deltaTime, Vector3.down);
    }
}
