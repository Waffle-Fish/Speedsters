using UnityEngine;

//Script for finish line that stops timer and player, sends to winEvent()
public class FinishLevel : MonoBehaviour
{   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player1")
        {
            Time_UI.Instance.isP1Finished = true;
            PlayerStop(collision.gameObject.GetComponent<PlayerController>());
        }
        else if (collision.tag == "Player2")
        {
            Time_UI.Instance.isP2Finished = true;
            PlayerStop(collision.gameObject.GetComponent<PlayerController>());

        }

        if (Time_UI.Instance.isP1Finished && Time_UI.Instance.isP2Finished)
        {
            winEvent();
        }
    }
    private void PlayerStop(PlayerController playerMovement)
    {
        //Removes momentum and disables PlayerController Script
        playerMovement.setVel(new Vector2(0, 0));
        playerMovement.enabled = false;

    }

    public void winEvent()
    {
        //getWinner() returns char ('1' or '2')
        if (Time_UI.Instance.getWinner() == '1')
        {
            Debug.Log("Winner is Player 1 with a time of " + Time_UI.Instance.getPlayer1Time() + "!");
        }
        else
        {
            Debug.Log("Winner is Player 2 with a time of " + Time_UI.Instance.getPlayer2Time() + "!");
        }
        
        
    }
}
