using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixBoxCollider2D : MonoBehaviour
{
    SpriteRenderer sr;
    BoxCollider2D bc;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();

        bc.size = new Vector2(sr.size.x, bc.size.y);
    }
}
