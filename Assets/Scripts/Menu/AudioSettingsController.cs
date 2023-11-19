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
        musicSlider.value = settings.MusicVolume;
        sfxSlider.value = settings.SFXVolume;
    }
    
    public void UpdateMusicVolume()
    {
        settings.MusicVolume = musicSlider.value;
        musicVolumeTMP.text = string.Format("{0:0}%", Mathf.Round(settings.MusicVolume * 100));
    }

    public void UpdateSFXVolume()
    {
        settings.SFXVolume = sfxSlider.value;
        sfxVolumeTMP.text = string.Format("{0:0}%", Mathf.Round(settings.SFXVolume * 100));
    }
}
