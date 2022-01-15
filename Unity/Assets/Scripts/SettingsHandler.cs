using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsHandler : MonoBehaviour
{
    public GameObject musicVolumeObject;
    private Text musicVolumeValue;
    public GameObject engineVolumeObject;
    private Text engineVolumeValue;
    public GameObject minimapObject;
    private Text minimapValue;

    public GameObject retroCameraObject;
    private Text retroCameraValue;
    
    private AudioSource[] audioSources; // 0:default, 1:up/set, 2:down, 3:reset

    public void Start()
    {
        musicVolumeValue = musicVolumeObject.GetComponent<Text>();
        musicVolumeValue.text = getMusicVolumePercentage();
        engineVolumeValue = engineVolumeObject.GetComponent<Text>();
        engineVolumeValue.text = getEngineVolumePercentage();
        minimapValue = minimapObject.GetComponent<Text>();
        minimapValue.text = getMinimapStatus();
        retroCameraValue = retroCameraObject.GetComponent<Text>();
        retroCameraValue.text = getRetroCameraStatus();
        audioSources = gameObject.GetComponents<AudioSource>();
    }

    public string getMusicVolumePercentage()
    {
        return Gameplay.musicVolume.ToString() + '%';
    }

    public string getEngineVolumePercentage()
    {
        return Gameplay.engineVolume.ToString() + '%';
    }

    public string getMinimapStatus()
    {
        string minimapStatus = Gameplay.minimapEnabled ? "On" : "Off";
        return minimapStatus;
    }

    public string getRetroCameraStatus()
    {
        string retroCameraStatus = Gameplay.retroCameraEnabled ? "On" : "Off";
        return retroCameraStatus;
    }

    public void increaseMusicVolume()
    {
        audioSources[1].Play();
        Gameplay.adjustScale("music", true);
        musicVolumeValue.text = getMusicVolumePercentage();
    }

    public void decreaseMusicVolume()
    {
        audioSources[2].Play();
        Gameplay.adjustScale("music", false);
        musicVolumeValue.text = getMusicVolumePercentage();
    }

    public void setMusicVolume(int volume)
    {
        audioSources[1].Play();
        Gameplay.setScale("music", volume);
        musicVolumeValue.text = getMusicVolumePercentage();
    }

    public void increaseEngineVolume()
    {
        audioSources[1].Play();
        Gameplay.adjustScale("engine", true);
        engineVolumeValue.text = getEngineVolumePercentage();

    }

    public void decreaseEngineVolume()
    {
        audioSources[2].Play();
        Gameplay.adjustScale("engine", false);
        engineVolumeValue.text = getEngineVolumePercentage();
    }

    public void setEngineVolume(int volume)
    {
        audioSources[1].Play();
        Gameplay.setScale("engine", volume);
        engineVolumeValue.text = getEngineVolumePercentage();
    }

    public void toggleMinimap()
    {
        audioSources[1].Play();
        Gameplay.toggle("minimap");
        minimapValue.text = getMinimapStatus();
    }

    public void setMinimap(bool value)
    {
        audioSources[1].Play();
        Gameplay.setToggle("minimap", value);
        minimapValue.text = getMinimapStatus();
    }

    public void toggleRetroCamera()
    {
        audioSources[1].Play();
        Gameplay.toggle("retroCamera");
        retroCameraValue.text = getRetroCameraStatus();
    }

    public void setRetroCamera(bool value)
    {
        audioSources[1].Play();
        Gameplay.setToggle("retroCamera", value);
        retroCameraValue.text = getRetroCameraStatus();
    }

    public void setDefault()
    {
        audioSources[3].Play();
        Gameplay.setScale("music", 100);
        musicVolumeValue.text = getMusicVolumePercentage();
        Gameplay.setScale("engine", 100);
        engineVolumeValue.text = getEngineVolumePercentage();
        Gameplay.setToggle("minimap", true);
        minimapValue.text = getMinimapStatus();
        Gameplay.setToggle("retroCamera", false);
        retroCameraValue.text = getRetroCameraStatus();
    }

    public void goToMainMenu()
	{
        audioSources[0].Play();
		SceneManager.LoadSceneAsync("Menu Scene");
	}
}
