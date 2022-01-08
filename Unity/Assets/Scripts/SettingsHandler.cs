using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsHandler : MonoBehaviour
{
    public GameObject musicVolumeObject;
    private Text musicVolumeValue;

    public GameObject retroCameraObject;
    private Text retroCameraValue;
    
    public void Start()
    {
        musicVolumeValue = musicVolumeObject.GetComponent<Text>();
        musicVolumeValue.text = getMusicVolumePercentage();
        retroCameraValue = retroCameraObject.GetComponent<Text>();
        retroCameraValue.text = getRetroCameraStatus();
    }

    public string getMusicVolumePercentage()
    {
        return Gameplay.musicVolume.ToString() + '%';
    }

    public string getRetroCameraStatus()
    {
        string retroCameraStatus = Gameplay.retroCameraEnabled ? "On" : "Off";
        return retroCameraStatus;
    }

    public void increaseMusicVolume()
    {
        Gameplay.incrementMusicVolume();
        musicVolumeValue.text = getMusicVolumePercentage();
    }

    public void decreaseMusicVolume()
    {
        Gameplay.decrementMusicVolume();
        musicVolumeValue.text = getMusicVolumePercentage();
    }

    public void toggleRetroCamera()
    {
        Gameplay.toggleRetroCamera();
        retroCameraValue.text = getRetroCameraStatus();
    }

    public void goToMainMenu()
	{
		SceneManager.LoadSceneAsync("Menu Scene");
	}
}
