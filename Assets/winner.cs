using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class winner : MonoBehaviour
{
    public Image winnerImage;
    Sprite player1Sprite = Player1Master.Instance.GetComponentsInChildren<SpriteRenderer>()[1].sprite;
    Sprite player2Sprite = Player2Master.Instance.GetComponentsInChildren<SpriteRenderer>()[1].sprite;
    void Start()
    {
        winnerImage = GetComponent<Image>();
        winnerImage.sprite = (Time_UI.Instance.GetWinner() == '1') ? player1Sprite : player2Sprite;
    }
}