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
    public abstract void ActivateEffect();

    protected SpriteRenderer itemSpriteRenderer;
    protected GameObject user;
    protected GameObject enemy;

    protected Camera userCamera;
    protected Camera enemyCamera;

    protected bool effectIsActive = false;

    #region StopWatch
    private float stopWatchTime = 0f;
    private bool stopWatchIsActive = false;

    protected bool GetStopWatchIsActive { get { return stopWatchIsActive; } }
    protected float GetStopWatchTime { get { return stopWatchTime; } }
    protected void StopStopWatch() { stopWatchIsActive = false; }
    protected void ResumeStopWatch() { stopWatchIsActive = true; }
    protected void ResetStopWatch() { stopWatchTime = 0f; }
    #endregion

    private void Start()
    {
        itemSpriteRenderer = GetComponent<SpriteRenderer>();
        
        // TODO: Display item
    }

    private void Update()
    {
        if (stopWatchIsActive)
        {
            stopWatchTime += Time.deltaTime;
        }
    }

    private void ProcessCollision()
    {
        effectIsActive = true;
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
