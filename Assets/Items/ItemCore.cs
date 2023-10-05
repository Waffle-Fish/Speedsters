using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ItemCore : MonoBehaviour
{
    public Item item;
    public abstract void ActivateEffect();

    protected SpriteRenderer spriteRenderer;
    protected Player1Master player1;
    protected Player2Master player2;

    protected GameObject user;
    protected GameObject enemy;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player1 = Player1Master.Instance;
        player2 = Player2Master.Instance;
    }

    private void ProcessCollision()
    {
        spriteRenderer.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2"))
        {
            if(other.gameObject.CompareTag("Player1"))
            {
                user = Player1Master.Instance.gameObject;
                enemy = Player2Master.Instance.gameObject;
            }
            else
            {
                user = Player2Master.Instance.gameObject;
                enemy = Player1Master.Instance.gameObject;
            }
            ProcessCollision();
            ActivateEffect();
        }
    }
}
