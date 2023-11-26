using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseGame : MonoBehaviour
{
    public static PauseGame Instance { get; private set; }
    public bool isGamePaused = false;
    public GameObject PauseMenuHUD;

    [SerializeField]
    [Tooltip("Adds more time to the game being paused between exiting the pause menu and resuming play of the game")]
    private float resumeDelayTime = 0f;
    float prevTimeScale = 0f;

    private Controls playerControls;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
        playerControls = new();
        playerControls.General.Enable();
        playerControls.General.PauseMenu.performed += ToggleMenu; 
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

    public void ToggleMenu(InputAction.CallbackContext context)
    {
        if(!context.performed) { return; }
        if(isGamePaused) { 
            Resume();
        }
        else 
        { 
            Pause(); 
        }
        PauseMenuHUD.SetActive(isGamePaused);
    }

    private IEnumerator ResumeGameDelay()
    {
        yield return new WaitForSeconds(resumeDelayTime);
    }
}
