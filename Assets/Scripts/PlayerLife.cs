using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    private Vector3 respawnPoint;

    void Start()
    {
        /* Initially sets respawn point at position spawned */
        respawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /* Any trigger object that is tagged "SpawnPoint" will update the respawn point*/
        if (collision.tag == "SpawnPoint")
        {
            respawnPoint = transform.position;
        }
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /* Any physical object that is tagged with Obstacle will respawn the player*/
        if (collision.gameObject.tag == "Obstacle")
        { 
            takeDamage(); 
        }
    }

    public void takeDamage()
    {
        transform.position = respawnPoint;
    }
}
