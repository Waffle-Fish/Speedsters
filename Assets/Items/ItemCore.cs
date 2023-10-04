using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemCore : MonoBehaviour
{
    protected Item item;

    public abstract void ActivateEffect();
}
