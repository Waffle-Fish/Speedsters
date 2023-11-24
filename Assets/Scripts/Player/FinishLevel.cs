using UnityEngine;

//Script for finish line that stops timer and player, sends to winEvent()
public class FinishLevel : MonoBehaviour
{   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1"))
        {
            Time_UI.Instance.isP1Finished = true;
            PlayerStop(collision.gameObject.GetComponent<PlayerController>());
        }
        else if (collision.CompareTag("Player2"))
        {
            Time_UI.Instance.isP2Finished = true;
            PlayerStop(collision.gameObject.GetComponent<PlayerController>());

        }

        if (Time_UI.Instance.isP1Finished && Time_UI.Instance.isP2Finished)
        {
            WinEvent();
        }
    }
    private void PlayerStop(PlayerController playerMovement)
    {
        //Removes momentum and disables PlayerController Script
        playerMovement.setVel(new Vector2(0, 0));
        playerMovement.finished = true;
        playerMovement.enabled = false;

    }

    public void WinEvent()
    {
        //getWinner() returns char ('1' or '2')
        if (Time_UI.Instance.GetWinner() == '1')
        {
            Debug.Log("Winner is Player 1 with a time of " + Time_UI.Instance.GetPlayer1Time() + "!");
        }
        else
        {
            Debug.Log("Winner is Player 2 with a time of " + Time_UI.Instance.GetPlayer2Time() + "!");
        }
        StartCoroutine(PostLevelProcess.Instance.StartProcess());
    }
}
