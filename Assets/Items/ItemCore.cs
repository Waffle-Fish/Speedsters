using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemCore : MonoBehaviour
{
    public Item item;
    public abstract void ActivateEffect();

    protected SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void ProcessCollision()
    {
        sr.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2"))
        {
            ProcessCollision();
            ActivateEffect();
        }
    }
}
