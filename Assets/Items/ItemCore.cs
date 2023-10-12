using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ItemCore : MonoBehaviour
{
    public Item item;
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
    protected PlayerMovement userMovement;
    protected PlayerMovement enemyMovement;
    protected PlayerLife userLife;
    protected PlayerLife enemyLife;
    protected Camera userCamera;
    protected Camera enemyCamera;
    protected SpriteRenderer userSpriteRenderer;
    protected SpriteRenderer enemySpriteRenderer;

    protected bool effectIsActive = false;

    #region Timer
    protected float timer = 0f;
    protected bool timerIsActive = false;
    protected void StopTimer() { timerIsActive = false; }
    protected void StartTimer() { timerIsActive = true; }
    protected void ResetTimer() { timer = 0f; }
    #endregion


    // Don't GetComponent<>() in start as it doesn't actually grab that items component
    private void Start()
    {
        

        // TODO: Display item
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        itemSpriteRenderer = GetComponent<SpriteRenderer>();
        itemSpriteRenderer.enabled = false;
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
        }
    }
    private void InitializeUserEnemy()
    {
        userCamera = user.GetComponentInChildren<Camera>();
        enemyCamera = enemy.GetComponentInChildren<Camera>();
        userTransform = user.GetComponent<Transform>();
        enemyTransform = enemy.GetComponent<Transform>();
        userRigidBody = user.GetComponent<Rigidbody2D>();
        enemyRigidBody = enemy.GetComponent<Rigidbody2D>();
        userMovement = user.GetComponent<PlayerMovement>();
        enemyMovement = enemy.GetComponent<PlayerMovement>();
        userLife = user.GetComponent<PlayerLife>();
        enemyLife = user.GetComponent <PlayerLife>();
        userSpriteRenderer = user.GetComponent<SpriteRenderer>();
        enemySpriteRenderer = enemy.GetComponent<SpriteRenderer>();
    }

    private IEnumerator ActivateEffectCoroutine()
    {
        yield return new WaitForSeconds(effectDelay);
        StartCoroutine(ActivateEffect());
    }

    private void ProcessCollision()
    {
        itemSpriteRenderer.enabled = false;
        StartCoroutine(ActivateEffectCoroutine());

        // TODO:
        //      IF slot full, auto use item in slot
        //      Put item into item slot
        //      This will need to call UI
    }
}
