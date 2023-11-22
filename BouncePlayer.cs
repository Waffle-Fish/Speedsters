using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePlayer : MonoBehaviour
{

public float bounceForce = 5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player1")
        {
            collision.gameObject.Get
            rb.velocity = new Vector2(rb.velocity.x, bounceForce)
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, bounceForce)
        }   
    }
}
