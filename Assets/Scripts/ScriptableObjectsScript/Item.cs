using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/ItemScriptableObject", order = 1)]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite groundAppearance;
    public Sprite slotAppearance;
    [Tooltip("Check if the item deals damage")]
    public bool doesDmg;
    public int dmgAmount;
    public List<Sprite> spritesDuringItemUse;

}
