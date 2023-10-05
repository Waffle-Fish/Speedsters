using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemCore : MonoBehaviour
{
    public Item item;
    public abstract void ActivateEffect();

    protected SpriteRenderer spriteRenderer;
    protected PlayerLife playerLife;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void ProcessCollision()
    {
        spriteRenderer.enabled = false;
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
