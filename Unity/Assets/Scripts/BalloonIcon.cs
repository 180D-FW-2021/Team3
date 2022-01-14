using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonIcon : MonoBehaviour
{
    void Start()
    {
        this.transform.position = new Vector3(this.transform.position.x, Gameplay.balloonIconHeight, this.transform.position.z);
    }
}
