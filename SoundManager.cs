using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioMixerGroup mixer;
    public Sound[] soundsArray;
    private SortedList<string, Sound> soundsList;


    private void Awake()
    {
        soundsList = new SortedList<string, Sound>();
        foreach (Sound s in soundsArray)
        {
            soundsList.Add(s.name, s);
        }

        foreach (KeyValuePair<string, Sound> s in soundsList)
        {
            s.Value.source = gameObject.AddComponent<AudioSource>();
            s.Value.source.volume = s.Value.volume;
            s.Value.source.clip = s.Value.clip;
            s.Value.source.loop = s.Value.isLoop;
            s.Value.source.outputAudioMixerGroup = mixer;
        }
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0) PlaySound("LevelMusic");
        else PlaySound("MenuMusic");
    }

    public void PlaySound(string key)
    {
        if (soundsList.ContainsKey(key)) soundsList[key].source.Play();
    }

    public void StopSound(string key)
    {
        if (soundsList.ContainsKey(key)) if(soundsList[key].source.isPlaying) soundsList[key].source.Stop();
    }
}
