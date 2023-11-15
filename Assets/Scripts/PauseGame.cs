using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public static PauseGame Instance { get; private set; }
    public bool isGamePaused = false;

    [SerializeField]
    [Tooltip("Adds more time to the game being paused between exiting the pause menu and resuming play of the game")]
    private float resumeDelayTime = 0f;
    float prevTimeScale = 0f;


    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }
    public void Pause()
    {
        prevTimeScale = Time.timeScale;
        Time.timeScale = 0;
        isGamePaused = true;
    }

    public void Resume()
    {
        StartCoroutine(ResumeGameDelay());
        Time.timeScale = prevTimeScale;
        isGamePaused = false;
    }

    private IEnumerator ResumeGameDelay()
    {
        yield return new WaitForSeconds(resumeDelayTime);
    }
}
