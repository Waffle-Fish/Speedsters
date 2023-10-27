using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public static PauseGame Instance { get; private set; }
    public static bool gameIsPaused = false;
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
        gameIsPaused = true;
    }

    public void Resume()
    {
        Time.timeScale = prevTimeScale;
        gameIsPaused = false;
    }
}
