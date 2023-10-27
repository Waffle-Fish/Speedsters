using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PreLevelSequence : MonoBehaviour
{
    public static PreLevelSequence Instance { get; private set; }
    [SerializeField] int screenTimeLength = 0;
    TextMeshProUGUI text;
    UnityEngine.UI.Image backdrop;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }

    private void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        backdrop = GetComponentInChildren<UnityEngine.UI.Image>();
        if(screenTimeLength <= 0) {
            text.gameObject.SetActive(false);
            backdrop.enabled = false;
            return; 
        }

        text.gameObject.SetActive(true);
        backdrop.enabled = true;
        PauseGame.Instance.Pause();
        StartCoroutine(CountdownScreen());
        
    }

    private IEnumerator CountdownScreen()
    {
        for (int i = screenTimeLength; i > 0; i--)
        {
            UpdateText(i);
            UpdateBackdrop(i);
            yield return new WaitForSecondsRealtime(1f);
        }
        PauseGame.Instance.Resume();
        text.gameObject.SetActive(false);
        backdrop.enabled = false;
        this.gameObject.SetActive(false);
    }

    private void UpdateText(int i)
    {
        text.text = i.ToString();
    }

    private void UpdateBackdrop(int i)
    {
        Color c = new(backdrop.color.r, backdrop.color.g, backdrop.color.b, i * backdrop.color.a / screenTimeLength);
        backdrop.color = c;
    }
}
