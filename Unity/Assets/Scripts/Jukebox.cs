using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Jukebox : MonoBehaviour
{
    public AudioClip[] songs;
    public int[] chances;
    public float[] volumes;

    private AudioSource jukebox;
    private int songNum;
    private AudioClip song;
    private float volume;

    void Start() 
    {
        GetJukebox();
        GetSongConfig();
        LoadSong();
        PlaySong();
    }

    void GetJukebox() // jukebox AudioSource should be the last AudioSource in an object
    {
        AudioSource[] allAudioSources = GetComponents<AudioSource>();
        AudioSource jukeboxAudioSource = allAudioSources[allAudioSources.Length - 1];
        jukebox = jukeboxAudioSource;
    }

    float[] GetChanceThresholds()
    {
        List<float> chanceThresholdsList = new List<float>();
        float totalChance = chances.Sum();
        foreach (float chance in chances)
        {
            chanceThresholdsList.Add(chance / totalChance + chanceThresholdsList.Sum());
        }
        float[] chanceThresholds = chanceThresholdsList.ToArray();
        return chanceThresholds;
    }

    void GetSongNum()
    {
        float[] chanceThresholds = GetChanceThresholds();
        float randomValue = Random.Range(0, 999);
        for (int thresholdIndex = 0; thresholdIndex < chanceThresholds.Length; thresholdIndex++)
        {
            if (randomValue < chanceThresholds[thresholdIndex] * 1000)
            {
                songNum =  thresholdIndex;
                break;
            }
        }
    }

    AudioClip GetSong()
    {
        AudioClip song = songs[songNum];
        return song;
    }

    float GetVolume()
    {
        float volume = volumes[songNum];
        return volume;
    }

    void GetSongConfig()
    {
        GetSongNum();
        song = GetSong();
        volume = GetVolume();
    }

    void LoadSong()
    {
        jukebox.clip = song;
        jukebox.volume = volume;
    }

    void PlaySong()
    {
        jukebox.Play();
    }
}