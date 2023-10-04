using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassTileBoxCollider2DFix : MonoBehaviour
{
    /// <Purpose>
    /// Works like BoxCollider2D auto tiling but dosen't include the grass as part of the hit box
    /// </Purpose>
    [SerializeField, Tooltip("Check if you want to stretch it horizontally and vertically")]
    bool stretchBoth;
    [SerializeField,Tooltip("Check if you want to stretch it horizontally, leave uncheck if you want to stretch it vertically")] 
    bool stretchHorizontal;
    SpriteRenderer sr;
    BoxCollider2D bc;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
        if (stretchBoth && stretchHorizontal) { 
            Debug.LogError(gameObject.name + "has stretchBoth & stretch Horizontal checked. Please uncheck one of them");
            return;
        }
        if(stretchBoth)
        {
            bc.size = new Vector2(sr.size.x, sr.size.y);
        }
        else if (stretchHorizontal)
        {
            bc.size = new Vector2(sr.size.x, bc.size.y);
        }
        else
        {
            bc.size = new Vector2(bc.size.x, sr.size.y);
        }
    }
}
