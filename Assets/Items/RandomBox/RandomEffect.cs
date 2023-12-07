using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomEffect : ItemCore
{
    public List<GameObject> itemEffectsList;
    private int randInd = 0;
    ItemCore randItem;

    public override IEnumerator ActivateEffect()
    {
        yield return null;
        StartCoroutine(randItem.ActivateEffect());
    }

    protected override void GetRandomEffect(Collider2D other)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        randInd = Random.Range(0, itemEffectsList.Count);
        sr.sprite = itemEffectsList[randInd].GetComponent<SpriteRenderer>().sprite; randItem = itemEffectsList[randInd].GetComponent<ItemCore>();
        randItem.DeclareIdentities(other);
        Debug.Log("You rolled a " + randInd + "\nYou got: " + itemEffectsList[randInd].name);
        return;
    }
}
