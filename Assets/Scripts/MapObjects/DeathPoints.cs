using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPoints : MonoBehaviour
{
    [SerializeField]
    public int damageDealt = 100;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            collision.gameObject.GetComponent<PlayerLife>().takeDamage(damageDealt);
        }
   
    }
}
