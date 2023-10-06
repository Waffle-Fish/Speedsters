using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ItemCore : MonoBehaviour
{
    public Item item;
    [Tooltip("How long it takes before the effect activates. Can be used for big spells that have long animation times. Choose 0 if no delay")]
    public float effectDelay = 0f;
    [Tooltip("How long the effect lasts. Choose 0 if no duration")]
    public float effectDuration = 0f;    
    public abstract void ActivateEffect();

    protected SpriteRenderer itemSpriteRenderer;
    protected GameObject user;
    protected GameObject enemy;

    protected Camera userCamera;
    protected Camera enemyCamera;

    protected float stopWatch;

    private void Start()
    {
        itemSpriteRenderer = GetComponent<SpriteRenderer>();
        
        // TODO: Display item
    }

    private void ProcessCollision()
    {
        itemSpriteRenderer.enabled = false;

        // TODO:
        //      IF slot full, auto use item in slot
        //      Put item into item slot
        //      This will need to call UI
    }

    private void InitializeUserEnemy()
    {
        userCamera = user.GetComponentInChildren<Camera>();
        enemyCamera = enemy.GetComponentInChildren<Camera>();
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
            InitializeUserEnemy();
            ProcessCollision();
            ActivateEffect();
            // Tell player that item is currently in use
        }
    }
}
