using UnityEngine;
using UnityEngine.InputSystem;

//Script that controls damage related hitbox operations
public class PlayerLife : MonoBehaviour
{
    [SerializeField]
    int health = 100;

    private Vector3 respawnPoint;

    void Start()
    {
        /* Initially sets respawn point at position spawned */
        respawnPoint = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /* Any trigger object that is tagged "SpawnPoint" will update the respawn point
           Prefab created for the trigger.                                              */
        if (collision.tag == "SpawnPoint")
        {
            respawnPoint = transform.position;
            Debug.Log("Spawn Point Updated");
        }
    }

 /*   private void OnCollisionEnter2D(Collision2D collision)
    {
        // Any physical object that is tagged with InstaObst will respawn the player with contact
        if (collision.gameObject.tag == "InstaObst")
        { 
            takeDamage(100);
        }
        // Any physical object that is tagged with RandomDmg will hurt the player with contact
        else if (collision.gameObject.tag == "RandomDmg")
        {
            takeDamage(20);
        }
    }
 */

    public void takeDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            transform.position = respawnPoint;
            health = 100;
        }
    }

    public void Die(InputAction.CallbackContext context)
    {
        takeDamage(health);
    }
}
