using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITiltOscillatiion : MonoBehaviour
{
    public float rotationFactor;
    public float rotationSpeed;
    public float skew;
    private float index = 0f;

    void Update()
    {
        index += Time.deltaTime;
        float rotation = rotationFactor * Mathf.Cos(index * rotationSpeed);
        transform.localEulerAngles += new Vector3(skew * rotation, skew * rotation, rotation);
    }
}
