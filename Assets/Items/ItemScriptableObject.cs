using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/ItemScriptableObject", order = 1)]
public class ItemScriptableObject : ScriptableObject
{
    public string itemName;
    public Sprite itemAppearance;

    /*Need to create an item interface and heiarchy for script to play the items*/
}
