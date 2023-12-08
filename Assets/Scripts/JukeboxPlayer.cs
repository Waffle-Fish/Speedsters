using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JukeboxPlayer : MonoBehaviour
{
    public static JukeboxPlayer Instance { get; private set; }
    [SerializeField]
    private SettingsScriptableObjects settings;
    [SerializeField]
    AudioClip intro;
    [SerializeField]
    AudioClip mainMusic;

    AudioSource audioSource;    

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        AudioListener.pause = false;
        UpdateVolume();
        ProcessMusicSequence();
    }

    public void UpdateVolume()
    {
        audioSource.volume = settings.MusicVolume;
    }

    private void ProcessMusicSequence()
    {
        audioSource.loop = true;
        audioSource.clip = mainMusic;
        if(PreLevelSequence.Instance)
        {
            audioSource.PlayDelayed(PreLevelSequence.Instance.ScreenTimeLength);
        }
        else
        {
             audioSource.Play();
        }
        
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
}
