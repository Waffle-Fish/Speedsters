using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsController : MonoBehaviour
{
    [SerializeField]
    private SettingsScriptableObjects settings;
    [SerializeField]
    private GameObject musicSliderObj;
    [SerializeField]
    private GameObject sfxSliderObj;
    [SerializeField]
    private GameObject musicVolumeTextObj;
    [SerializeField]
    private GameObject sfxVolumeTextObj;

    private Slider musicSlider;
    private Slider sfxSlider;
    private TextMeshProUGUI musicVolumeTMP;
    private TextMeshProUGUI sfxVolumeTMP;


    private void Awake()
    {
        musicSlider = musicSliderObj.GetComponent<Slider>();
        sfxSlider = sfxSliderObj.GetComponent<Slider>();
        musicVolumeTMP = musicVolumeTextObj.GetComponent<TextMeshProUGUI>();
        sfxVolumeTMP = sfxVolumeTextObj.GetComponent<TextMeshProUGUI>();
        UpdateSliderAndText();

    }

    private void UpdateSliderAndText()
    {
        musicSlider.value = settings.MusicVolume;
        sfxSlider.value = settings.SFXVolume;
        musicVolumeTMP.text = string.Format("{0:0}%", musicSlider.value * 100);
        sfxVolumeTMP.text = string.Format("{0:0}%", sfxSlider.value * 100);
    }

    public void UpdateVolume()
    {
        settings.MusicVolume = musicSlider.value;
        settings.SFXVolume = sfxSlider.value;
        UpdateSliderAndText();
    }
}
