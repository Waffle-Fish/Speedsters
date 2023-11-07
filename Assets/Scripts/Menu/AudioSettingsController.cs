using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsController : MonoBehaviour
{
    [SerializeField]
    private SettingsScriptableObjects settings;
    [SerializeField]
    private GameObject musicSlider;
    [SerializeField]
    private GameObject sfxSlider;

    private float musicVolume = 0f;
    private float sfxVolume = 0f;

    private void Awake()
    {
        musicVolume = settings.MusicVolume;
        sfxVolume = settings.SFXVolume;
        musicSlider.GetComponent<Slider>().value = musicVolume;
        sfxSlider.GetComponent<Slider>().value = sfxVolume;
    }
    private void Start() 
    {

    }
}
