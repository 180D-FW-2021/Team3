using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaytimeHandler : MonoBehaviour
{
    public DaytimePreset sunset;
    public DaytimePreset dusk;
    public DaytimePreset dawn;
    public DaytimePreset day;
    private Light lightSettings;

    void Awake()
    {
        lightSettings = gameObject.GetComponent<Light>();
        switch (Gameplay.daytime)
        {
            case "sunset":
                RenderSettings.skybox = sunset.skybox;
                RenderSettings.ambientSkyColor = sunset.ambientColor;
                lightSettings.color = sunset.lightColor;
                lightSettings.intensity = sunset.lightIntensity;
                sunset.jukebox.SetActive(true);
                break;
            case "dusk":
                RenderSettings.skybox = dusk.skybox;
                RenderSettings.ambientSkyColor = dusk.ambientColor;
                lightSettings.color = dusk.lightColor;
                lightSettings.intensity = dusk.lightIntensity;
                dusk.jukebox.SetActive(true);
                break;
            case "dawn":
                RenderSettings.skybox = dawn.skybox;
                RenderSettings.ambientSkyColor = dawn.ambientColor;
                lightSettings.color = dawn.lightColor;
                lightSettings.intensity = dawn.lightIntensity;
                dawn.jukebox.SetActive(true);
                break;
            case "day":
                RenderSettings.skybox = day.skybox;
                RenderSettings.ambientSkyColor = day.ambientColor;
                lightSettings.color = day.lightColor;
                lightSettings.intensity = day.lightIntensity;
                day.jukebox.SetActive(true);
                break;
            default:
                RenderSettings.skybox = sunset.skybox;
                RenderSettings.ambientSkyColor = sunset.ambientColor;
                lightSettings.color = sunset.lightColor;
                lightSettings.intensity = sunset.lightIntensity;
                sunset.jukebox.SetActive(true);
                break;
        }
    }
}

[System.Serializable]
public struct DaytimePreset
{
    public Material skybox;
    [ColorUsage(true, true)]
    public Color ambientColor;
    public Color lightColor;
    public float lightIntensity;
    public GameObject jukebox;
}
