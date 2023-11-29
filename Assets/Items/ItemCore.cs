using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ItemCore : MonoBehaviour
{
    //public Item item;
    [Tooltip("In seconds, How long it takes before the effect activates. Choose 0 for no delay. Can be used for big spells that have long animation times. ")]
    public float effectDelay = 0f;
    [Tooltip("In seconds, How long the effect lasts. Choose 0 for no duration")]
    public float effectDuration = 0f;
    public abstract IEnumerator ActivateEffect();

    protected SpriteRenderer itemSpriteRenderer;
    protected GameObject user;
    protected GameObject enemy;
    protected Transform userTransform;
    protected Transform enemyTransform;
    protected Rigidbody2D userRigidBody;
    protected Rigidbody2D enemyRigidBody;
    protected PlayerController userMovement;
    protected PlayerController enemyMovement;
    protected PlayerLife userLife;
    protected PlayerLife enemyLife;
    protected Camera userCamera;
    protected Camera enemyCamera;
    protected SpriteRenderer userSpriteRenderer;
    protected SpriteRenderer enemySpriteRenderer;
    protected GameObject userScreen;
    protected GameObject enemyScreen;

    protected bool effectIsActive = false;

    #region Timer
    protected float timer = 0f;
    protected bool timerIsActive = false;
    protected void StopTimer() { timerIsActive = false; }
    protected void StartTimer() { timerIsActive = true; }
    protected void ResetTimer() { timer = 0f; }
    #endregion

    #region RandomBox
    protected virtual void GetRandomEffect(Collider2D other){ return; }

    #endregion

    // Don't GetComponent<>() in start as it doesn't actually grab that itemCore child's component

    private void OnTriggerEnter2D(Collider2D other)
    {
        DeclareIdentities(other);
        itemSpriteRenderer = GetComponent<SpriteRenderer>();
        ProcessPickup(other);
    }

    public void DeclareIdentities(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player1"))
        {
            user = Player1Master.Instance.gameObject;
            enemy = Player2Master.Instance.gameObject;
            userScreen = TopScreenUIManager.Instance.gameObject;
            enemyScreen = BottomScreenUIManager.Instance.gameObject;
        }
        else
        {
            user = Player2Master.Instance.gameObject;
            enemy = Player1Master.Instance.gameObject;
            userScreen = BottomScreenUIManager.Instance.gameObject;
            enemyScreen = TopScreenUIManager.Instance.gameObject;
        }
        InitializeUserEnemy();
    }

    private void InitializeUserEnemy()
    {
        userCamera = user.GetComponentInChildren<Camera>();
        enemyCamera = enemy.GetComponentInChildren<Camera>();
        userTransform = user.GetComponent<Transform>();
        enemyTransform = enemy.GetComponent<Transform>();
        userRigidBody = user.GetComponent<Rigidbody2D>();
        enemyRigidBody = enemy.GetComponent<Rigidbody2D>();
        userMovement = user.GetComponent<PlayerController>();
        enemyMovement = enemy.GetComponent<PlayerController>();
        userLife = user.GetComponent<PlayerLife>();
        enemyLife = user.GetComponent <PlayerLife>();
        userSpriteRenderer = user.GetComponent<SpriteRenderer>();
        enemySpriteRenderer = enemy.GetComponent<SpriteRenderer>();
    }

    private void ProcessPickup(Collider2D other)
    {
        PlayerUseItem userPUI = user.GetComponent<PlayerUseItem>();
        ItemCore userItem = userPUI.item;

        if(!userItem)
        {
            itemSpriteRenderer.enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            Debug.Log("Tag: " + this.tag);
            if(this.CompareTag("RandomBox"))
            {
                GetRandomEffect(other);
                InitializeUserEnemy();
            }
            userPUI.item = this;
            userPUI.itemSprite = itemSpriteRenderer.sprite;
            userPUI.DisplayItemSprite();
        }
    }

    public IEnumerator DelayEffect()
    {
        yield return new WaitForSeconds(effectDelay);
        StartCoroutine(ActivateEffect());
    }
}
