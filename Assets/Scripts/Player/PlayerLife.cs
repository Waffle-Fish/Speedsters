using UnityEngine;


//Script that controls hitbox operations
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
        else if(collision.tag == "Finish")
        {
            if (this.tag == "Player1")          //Player 1 is finished and gets frozen 
            {
                Time_UI.Instance.isP1Finished = true;

                //Removes momentum and disables PlayerController Script
                PlayerController p1 = Player1Master.Instance.GetComponent<PlayerController>();

                p1.setVel(new Vector2(0, 0));
                p1.enabled = false;
                
                Debug.Log("Player 1 Finished");
            }
            else if(this.tag == "Player2")      //Player 2 is finished and gets frozen 
            {
                Time_UI.Instance.isP2Finished = true;
                
                //Removes momentum and disables PlayerController Script
                PlayerController p2 = Player2Master.Instance.GetComponent<PlayerController>();
                p2.setVel(new Vector2(0, 0));
                p2.enabled = false;

                Debug.Log("Player 2 Finished");
            }

            //Calls winEvent() if both finished
            if (Time_UI.Instance.isP1Finished && Time_UI.Instance.isP2Finished)
            {
                winEvent();
            }
            
            /* Freezes player and timer, uses check variable to see if both players have finished, if so, then player
            with least time wins  -> sends to win screen with scoreboard maybe or menu screen? Freeze player function can be made
            for start as well if we want a countdown maybe                                     */
        }

    }

/*   private void OnCollisionEnter2D(Collision2D collision)
    {
        // Any physical object that is tagged with InstaObst will respawn the player with contact
        if (collision.gameObject.tag == "InstaObst")
        { 
          //  takeDamage(100);
        }
        // Any physical object that is tagged with RandomDmg will hurt the player with contact
        else if (collision.gameObject.tag == "RandomDmg")
        {
           // takeDamage(20);
        }
 } */

    public void takeDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            transform.position = respawnPoint;
            health = 100; // - Jesse
        }
    }

    private void winEvent()
    {
        //getWinner() returns char ('1' or '2')
        Debug.Log("Winner is Player " + Time_UI.Instance.getWinner() + "!");
    }
}
