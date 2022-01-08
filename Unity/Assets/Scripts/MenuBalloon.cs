using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBalloon : MonoBehaviour
{
    public float index = 0f;
    public float deltaHeight;
    public float modifier;
    private RectTransform location;

    // Start is called before the first frame update
    void Start()
    {
        index = UnityEngine.Random.Range(0f, 6.28f);
        location = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        index += Time.deltaTime;
        float y = modifier * deltaHeight * Mathf.Sin(index);
        location.anchoredPosition = new Vector2(location.anchoredPosition.x, location.anchoredPosition.y + y);
    }
}
