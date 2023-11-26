using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUseItem : MonoBehaviour
{
    public ItemCore item;
    public void UseItem(InputAction.CallbackContext context)
    {
        if(!context.performed && !item) { return; }
        StartCoroutine(item.DelayEffect());
    }
}
