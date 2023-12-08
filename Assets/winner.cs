using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class winner : MonoBehaviour
{
    public Image winnerImage;
    Sprite player1Sprite = Player1Master.Instance.GetComponentsInChildren<SpriteRenderer>()[1].sprite;
    void Start()
    {
        winnerImage.sprite = player1Sprite;
    }
}