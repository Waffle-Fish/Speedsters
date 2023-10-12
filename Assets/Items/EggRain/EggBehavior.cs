using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggBehavior : MonoBehaviour
{
    public bool HitPlayer { get; private set; } = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player1") || collision.transform.CompareTag("Player2"))
        {
            HitPlayer = true;
        }
    }
}
