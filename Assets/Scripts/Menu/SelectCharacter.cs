using System;
using UnityEngine;
using UnityEngine.UI;

public class SelectCharacter : MonoBehaviour
{
    public static SelectCharacter Instance { get; private set; }

    private int[] index = new int[] {
       0, 0
    };

    [SerializeField]
    Sprite[] imageList;
    [SerializeField]
    Image[] charImages;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }

    //Increases image index, made it easier to read so just send 1/2 for player
    public void incIndex(int player)
    {
        if (index[player-1]+1 == imageList.Length)
        {
            index[player-1] = 0;
        }
        else
        { index[player-1]++; }

        charImages[player-1].sprite = imageList[index[player-1]];
    }

    //Decreases image index, for both functions, it'll loop around
    public void decIndex(int player)
    {
        if (index[player-1] -1 <= -1)
        {
            index[player-1] = imageList.Length-1;
        }
        else
        { index[player-1]--; }

        charImages[player-1].sprite = imageList[index[player-1]];
    }

}
