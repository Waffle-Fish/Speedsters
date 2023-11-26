using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerUseItem : MonoBehaviour
{
    public Sprite itemSprite;
    public ItemCore item;
    public Image itemToDisplay;

    private Color transparent = new(255,255,255,0);
    private void Start()
    {
        DisplayItemSprite();
    }
    public void UseItem(InputAction.CallbackContext context)
    {
        if(!context.performed || !item) { return; }
        itemSprite = null;
        DisplayItemSprite();
        StartCoroutine(item.DelayEffect());
        item = null;
    }

    public void DisplayItemSprite()
    {
        itemToDisplay.color = (!itemSprite) ? transparent : Color.white;
        itemToDisplay.sprite = itemSprite;
    }
}
