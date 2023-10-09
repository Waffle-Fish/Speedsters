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

    protected Camera userCamera;
    protected Camera enemyCamera;

    protected bool effectIsActive = false;

    #region Timer
    protected float timer = 0f;
    protected bool timerIsActive = false;
    protected void StopTimer() { timerIsActive = false; }
    protected void StartTimer() { timerIsActive = true; }
    protected void ResetTimer() { timer = 0f; }
    #endregion

    private void Start()
    {
        itemSpriteRenderer = GetComponent<SpriteRenderer>();
        Debug.Log("itemSpriteRenderer: " + itemSpriteRenderer.name);

        // TODO: Display item
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

    private IEnumerator ActivateEffectCoroutine()
    {
        yield return new WaitForSeconds(effectDelay);
        StartCoroutine(ActivateEffect());
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
            // Tell player that item is currently in use
        }
    }
}
