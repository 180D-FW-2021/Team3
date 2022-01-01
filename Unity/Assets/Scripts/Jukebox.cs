using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Jukebox : MonoBehaviour
{
    public Song[] music;

    private System.Random randomGenerator;
    private AudioSource jukebox;
    private Song chosenSong;

    void Start()
    {
        randomGenerator = new System.Random(); 
        jukebox = GetJukebox();
        chosenSong = GetChosenSong();
        PlayChosenSong();
    }

    public AudioSource GetJukebox() //jukebox AudioSource should be the last AudioSource in an object
    { 
        AudioSource[] allAudioSources = GetComponents<AudioSource>();
        AudioSource jukeboxAudioSource = allAudioSources[allAudioSources.Length - 1];
        return jukeboxAudioSource;
    }

    public Song GetChosenSong() //find a song according to the chance of playing
    {
        double randomValue = randomGenerator.NextDouble();
        double accumulatedChance = 0.0;
        int totalChance = GetTotalChance();
        for (int songIndex = 0; songIndex < music.Length; songIndex++)
        {
            accumulatedChance += music[songIndex].chance / (double)totalChance;
            if (randomValue < accumulatedChance)
            {
                return music[songIndex];
            }
        }
        return music[0];
    }

    public int GetTotalChance() //find the combined sum of chances for normalization
    {
        int totalChance = 0;
        foreach (Song songItem in music)
        {
            totalChance += songItem.chance;
        }
        return totalChance;
    }

    public void PlayChosenSong() //load a song into the jukebox and play it
    {
        jukebox.clip = chosenSong.song;
        jukebox.volume = chosenSong.volume;
        jukebox.Play();
    }
}

[System.Serializable]
public struct Song
{
    public AudioClip song;
    public int chance;
    public float volume;
}