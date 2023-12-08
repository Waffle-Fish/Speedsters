using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PostLevelProcess : MonoBehaviour
{
    [SerializeField]
    float initialDelay = 0f;
    [SerializeField]
    GameObject PlayerTextGO;
    public Sprite p1Sprite;
    public Sprite p2Sprite;
    public UnityEngine.UI.Image winnerImg;
    public UnityEngine.UI.Image loserImg;
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
        SetPodiumPictures();
        for (int i = 1; i < rectTransforms.Count; i++)
        {
            rectTransforms[i].gameObject.SetActive(true);
        }
    }  

    private void SetPodiumPictures()
    {
        winnerImg.sprite = (Time_UI.Instance.GetWinner() == '1') ? p1Sprite : p2Sprite;
        loserImg.sprite = (Time_UI.Instance.GetWinner() == '1') ? p2Sprite : p1Sprite;
    }
}
