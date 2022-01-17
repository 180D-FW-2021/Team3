using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotAirBalloons : MonoBehaviour
{
    public ColorProfile[] colorProfiles;
    private MeshRenderer[] childRenderers;
    private bool rotatedToWind = false;
    private Vector3 direction;
    private float hotAirBalloonMovementSpeed;

    void Start()
    {
        direction = new Vector3(Random.Range(-.15f,0f), Random.Range(0f, .1f), Random.Range(-.15f, 0f));
        hotAirBalloonMovementSpeed = Gameplay.hotAirBalloonMovementSpeed;
        childRenderers = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer childRenderer in childRenderers)
        {
            int randomColorProfileIndex = Random.Range(0, colorProfiles.Length -1);
            Material[] balloonMaterials = childRenderer.materials;
            if (balloonMaterials.Length == 4)
            {
                balloonMaterials[0] = colorProfiles[randomColorProfileIndex].color1;
                balloonMaterials[1] = colorProfiles[randomColorProfileIndex].color2;
                childRenderer.materials = balloonMaterials;
            }
        }
    }

    void Update()
    {
        if (!rotatedToWind)
        {
            rotatedToWind = true;
            this.transform.Rotate(new Vector3(0,Mathf.Atan(SpawnBalloons.wind.x / SpawnBalloons.wind.z) * 180 / Mathf.PI + 180,0));
        }
        if (!Gameplay.isPaused)
        {
            transform.localPosition = this.transform.localPosition + direction * hotAirBalloonMovementSpeed * Time.deltaTime;
        }
    }
}

[System.Serializable]
public struct ColorProfile
{
    public Material color1;
    public Material color2;
}