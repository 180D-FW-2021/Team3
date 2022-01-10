using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonIcon : MonoBehaviour
{
    public int iconHeight;

    void Start()
    {
        this.transform.position = new Vector3(this.transform.position.x, iconHeight, this.transform.position.z);
    }
}
