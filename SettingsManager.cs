using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class SettingsManager : MonoBehaviour
{

    public Slider volume;
    public Slider brightness;
    public AudioMixer audioMixer;
    public GameObject brightnessPanel;

    private float currentVolume;

    // Start is called before the first frame update
    void Start()
    {
        brightnessPanel = GameObject.FindGameObjectWithTag("Brightness");
        audioMixer.GetFloat("MasterVolume", out currentVolume);
        volume.value = currentVolume;
        brightness.value = PlayerPrefs.GetFloat("Brightness", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetVolume()
    {
        audioMixer.SetFloat("MasterVolume", volume.value);
    }

    public void SetBrightness()
    {
        brightnessPanel.GetComponent<Image>().color = new Color(0, 0, 0, 1 - brightness.value);
        PlayerPrefs.SetFloat("Brightness", brightness.value);
    }
}
