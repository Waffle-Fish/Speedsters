using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class ReadyButton : MonoBehaviour
{
    public static ReadyButton Instance { get; private set; }

    public bool p1ready = false;
    public bool p2ready = false;

    [SerializeField]
    Sprite unreadyButtonImg;
    [SerializeField]
    Sprite readyButtonImg;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }

    //Changes state of ready button and also causes button color to change
    public void readyP1(Button b)
    {
        p1ready = !p1ready;

        changeButtonColor(p1ready, b);
    }

    public void readyP2(Button b)
    {
        p2ready = !p2ready;
        changeButtonColor(p2ready, b);

    }

    public void changeButtonColor(bool state, Button b)
    {
        ColorBlock block = b.colors;

        if (state)
        {
            block.selectedColor = UnityEngine.Color.HSVToRGB(.138F, 0.93F, 1F);
            block.normalColor = UnityEngine.Color.HSVToRGB(.138F, 0.93F, 1F);
            
        }
        else
        {
            block.selectedColor = Color.white;
            block.normalColor = Color.white;
            
        }
        b.colors = block;
    }
    
    public void playButtonCheck(Button b)
    {
        if (p1ready && p2ready && CharacterSelectMenu.Instance.getIsPlayer2())
        {
            b.image.sprite = readyButtonImg;
        }
        else if (p1ready && !CharacterSelectMenu.Instance.getIsPlayer2())
        {
            b.image.sprite = readyButtonImg;
        }
        else
        {
            b.image.sprite = unreadyButtonImg;
        }
    }

    //Resets buttons in case of back out
    public void reset1Button(Button b1)
    {
        p1ready = false;
        changeButtonColor(p1ready, b1);
    }

    public void reset2Button(Button b2)
    {
        p2ready = false;
        changeButtonColor(p2ready, b2);
    }
}
