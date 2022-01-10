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
        Gameplay.adjustScale("music", true);
        musicVolumeValue.text = getMusicVolumePercentage();
    }

    public void decreaseMusicVolume()
    {
        Gameplay.adjustScale("music", false);
        musicVolumeValue.text = getMusicVolumePercentage();
    }

    public void setMusicVolume(int volume)
    {
        Gameplay.setScale("music", volume);
        musicVolumeValue.text = getMusicVolumePercentage();
    }

    public void increaseEngineVolume()
    {
        Gameplay.adjustScale("engine", true);
        engineVolumeValue.text = getEngineVolumePercentage();

    }

    public void decreaseEngineVolume()
    {
        Gameplay.adjustScale("engine", false);
        engineVolumeValue.text = getEngineVolumePercentage();
    }

    public void setEngineVolume(int volume)
    {
        Gameplay.setScale("engine", volume);
        engineVolumeValue.text = getEngineVolumePercentage();
    }

    public void toggleMinimap()
    {
        Gameplay.toggle("minimap");
        minimapValue.text = getMinimapStatus();
    }

    public void setMinimap(bool value)
    {
        Gameplay.setToggle("minimap", value);
        minimapValue.text = getMinimapStatus();
    }

    public void toggleRetroCamera()
    {
        Gameplay.toggle("retroCamera");
        retroCameraValue.text = getRetroCameraStatus();
    }

    public void setRetroCamera(bool value)
    {
        Gameplay.setToggle("retroCamera", value);
        retroCameraValue.text = getRetroCameraStatus();
    }

    public void setDefault()
    {
        setMusicVolume(100);
        setEngineVolume(100);
        setMinimap(true);
        setRetroCamera(false);
    }

    public void goToMainMenu()
	{
		SceneManager.LoadSceneAsync("Menu Scene");
	}
}
