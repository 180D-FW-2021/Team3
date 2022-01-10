using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapHandler : MonoBehaviour
{
    void Start()
    {
        if (!Gameplay.minimapEnabled)
        {
            this.gameObject.SetActive(false);
        }
    }
}
