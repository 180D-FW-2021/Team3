using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    private Vector3 heightNormalizer;
    public float speed;

    void Start()
    {
        heightNormalizer = new Vector3(1f, 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Gameplay.isPaused)
        {
            transform.localPosition = this.transform.localPosition + Vector3.Scale(this.transform.forward, heightNormalizer) * speed * Time.deltaTime;
        }
    }
}
