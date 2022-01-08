using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniCamFollow : MonoBehaviour
{

    public Transform Plane;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        transform.position = Plane.position + offset;

        //rotation
        Vector3 rot = new Vector3(90, Plane.eulerAngles.y, 0);
        transform.rotation = Quaternion.Euler(rot);
        
    }
}
