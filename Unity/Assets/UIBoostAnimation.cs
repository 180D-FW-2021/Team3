using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBoostAnimation : MonoBehaviour
{
    private float moveIndex = 0f;
    private float scaleIndex = 0f;
    private int pauseCounter = 0;
    public int direction = -1;
    public float moveDistance;    
    public float moveSpeed;
    public float maxScale;
    public float scaleSpeed;
    public int pauseFrames;
    private Vector3 basePosition;
    private Vector3 baseScale;

    void Start()
    {
        basePosition = transform.localPosition;
        baseScale = transform.localScale;
    }

    void Update()
    {
        if (pauseCounter != 0)
        {
            pauseCounter--;
            moveIndex = 0f;
            scaleIndex = 0f;
        }
        else
            {
            moveIndex += Time.deltaTime * moveSpeed;
            scaleIndex += Time.deltaTime * scaleSpeed;
            if (moveIndex > moveDistance)
            {
                pauseCounter = pauseFrames;
                // moveIndex = 0f;
                // scaleIndex = 0f;
            }
            if (scaleIndex > maxScale)
            {
                pauseCounter = pauseFrames;
                // scaleIndex = 0f;
                // moveIndex = 0f;
            }
            transform.localPosition = basePosition + new Vector3(moveIndex * direction, 0, 0);
            transform.localScale = baseScale + new Vector3(-1 * scaleIndex, scaleIndex, scaleIndex);
        }
    }
}
