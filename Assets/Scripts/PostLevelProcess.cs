using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PostLevelProcess : MonoBehaviour
{
    [SerializeField]
    float initialDelay = 0f;
    [SerializeField]
    GameObject PlayerTextGO;
    [SerializeField]

    public static PostLevelProcess Instance { get; private set; }
    List<RectTransform> rectTransforms = new();
    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }

        //
        GetComponentsInChildren<RectTransform>(rectTransforms);
        for(int i = 1; i < rectTransforms.Count; i++)
        {
            rectTransforms[i].gameObject.SetActive(false);
        }
    }

    public IEnumerator StartProcess() {
        yield return new WaitForSecondsRealtime(initialDelay); // Can add a slight delay between player finishing and finish screen
        PauseGame.Instance.Pause();
        TextMeshProUGUI PlayerTMP = PlayerTextGO.GetComponent<TextMeshProUGUI>();
        PlayerTMP.text = string.Format("Player {0}!",Time_UI.Instance.GetWinner());
        for (int i = 1; i < rectTransforms.Count; i++)
        {
            rectTransforms[i].gameObject.SetActive(true);
        }
    }   
}
