using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class freezeItem : MonoBehaviour
{

    public bool playerOneIsFrozen = false;
    public bool playerTwoIsFrozen = false;
    public float freezeDuration = 2.0f;
    Rigidbody2D rb1;
    Rigidbody2D rb2;
    float _playerOnePendingFreezeDuration = 0f;
    float _playerTwoPendingFreezeDuration = 0f;

    void Start()
    {
        rb1 = GetComponent<Rigidbody2D>();
        rb2 = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //if (_pendingFreezeDuration != 0f && !playerIsFrozen)
        //{

        //}
    }

    IEnumerator ItemEffect(int playerEffected)
    {

        if (playerEffected == 1)
        {
            _playerOnePendingFreezeDuration = freezeDuration;
            rb1.constraints = RigidbodyConstraints2D.FreezePosition;
            playerOneIsFrozen = true;

            var original = Time.timeScale;
            Time.timeScale = 0f;
            yield return new WaitForSecondsRealtime(freezeDuration);

            Time.timeScale = original;
            _playerOnePendingFreezeDuration = 0f;
            playerOneIsFrozen = false;
        }

        if (playerEffected == 2)
        {
            _playerTwoPendingFreezeDuration = freezeDuration;
            rb2.constraints = RigidbodyConstraints2D.FreezePosition;
            playerTwoIsFrozen = true;

            var original = Time.timeScale;
            Time.timeScale = 0f;
            yield return new WaitForSecondsRealtime(freezeDuration);

            Time.timeScale = original;
            _playerTwoPendingFreezeDuration = 0f;
            playerTwoIsFrozen = false;

        }
    }
}
