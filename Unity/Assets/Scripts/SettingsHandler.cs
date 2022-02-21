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
    public GameObject timeOfDayObject;
    private Text timeOfDayValue;
    
    private AudioSource[] audioSources; // 0:default, 1:up/set, 2:down, 3:reset
    public GameObject jukeboxObject;
    private AudioSource jukeboxPlayer;
    private Jukebox jukebox;

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
        timeOfDayValue = timeOfDayObject.GetComponent<Text>();
        timeOfDayValue.text = getTimeOfDayStatus();
        audioSources = gameObject.GetComponents<AudioSource>();
        jukeboxPlayer = jukeboxObject.GetComponent<AudioSource>();
        jukebox = jukeboxObject.GetComponent<Jukebox>();
        //getPlayerPreferences();
    }

    public void Update()
    {
        jukeboxPlayer.volume = jukebox.originalVolume * Gameplay.musicVolume / 100f;
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

    public string getTimeOfDayStatus()
    {
        string timeOfDayStatus = char.ToUpper(Gameplay.daytime[0]) + Gameplay.daytime.Substring(1);
        if (Gameplay.daytime == "sunset")
        {
            timeOfDayValue.fontSize = 40;
        }
        else
        {
            timeOfDayValue.fontSize = 50;
        }
        return timeOfDayStatus;
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

    public void increaseTimeOfDay()
    {
        audioSources[1].Play();
        Gameplay.adjustTimeOfDay(true);
        timeOfDayValue.text = getTimeOfDayStatus();
    }

    public void decreaseTimeOfDay()
    {
        audioSources[2].Play();
        Gameplay.adjustTimeOfDay(false);
        timeOfDayValue.text = getTimeOfDayStatus();
    }
    
    public void setTimeOfDay(string timeOfDay)
    {
        audioSources[1].Play();
        Gameplay.setTimeOfDay(timeOfDay);
        timeOfDayValue.text = getTimeOfDayStatus();
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
        Gameplay.setTimeOfDay("sunset");
        timeOfDayValue.text = getTimeOfDayStatus();
    }

    public void goToMainMenu()
	{
        audioSources[0].Play();
        savePlayerPreferences();
		SceneManager.LoadSceneAsync("Menu Scene");
	}

    public void savePlayerPreferences() {
        StartCoroutine(WebAPIAccess.UpdatePlayerPreferences(Player.username, Gameplay.musicVolume, Gameplay.engineVolume, Gameplay.minimapEnabled, Gameplay.retroCameraEnabled, Gameplay.daytime));
    }
}

// [System.Serializable]
// public class PreferenceProfile
// {
//     public int music_volume;
//     public int engine_volume;
//     public int minimap;
//     public int retro_camera;
//     public string daytime;

//     public bool intToBool(int value)
//     {
//         return (value == 1);
//     }
// }