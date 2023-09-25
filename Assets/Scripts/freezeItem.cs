using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class freezeItem : MonoBehaviour
{

    public bool playerOneIsFrozen = false;
    public bool playerTwoIsFrozen = false;
    
    public Rigidbody2D rb1;
    public Rigidbody2D rb2;

    float _playerOnePendingFreezeDuration = 0f;
    float _playerTwoPendingFreezeDuration = 0f;

    public float freezeDuration = 2.0f;

    public GameObject itemObj;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("triggered");
        if (other.gameObject.CompareTag("Player1") )
        {

            itemObj.SetActive(false);
            Debug.Log("calling function");
            ItemEffect(2);                // this function call isn't working
            Debug.Log("function passed");
            // Destroy(gameObject);


        }
        if (other.gameObject.CompareTag("Player2"))
        {
            //disable visual of object
            ItemEffect(1);              // this function call isn't working
            Destroy(gameObject);
        }
    }

    IEnumerator ItemEffect(int playerEffected)
    {
        Debug.Log("item effect");
        if (playerEffected == 1)
        {
            Debug.Log("player 1 effected");
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
            Debug.Log("player 2 effected");

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
