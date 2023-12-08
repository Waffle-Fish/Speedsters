using UnityEngine;
using UnityEngine.SceneManagement;

public class UPFinishLevel : MonoBehaviour
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
        playerMovement.setVel(new Vector2(0, 0));
        playerMovement.enabled = false;
    }

    public void WinEvent()
    {
        //getWinner() returns char ('1' or '2')
        if (Time_UI.Instance.GetWinner() == '1')
        {
            Debug.Log("Winner is Player 1 with a time of " + Time_UI.Instance.GetPlayer1Time() + "!");
            Sprite player1Sprite = Player1Master.Instance.GetComponentsInChildren<SpriteRenderer>()[1].sprite;
        }
        else
        {
            Debug.Log("Winner is Player 2 with a time of " + Time_UI.Instance.GetPlayer2Time() + "!");
            Sprite player2Sprite = Player1Master.Instance.GetComponentsInChildren<SpriteRenderer>()[1].sprite;
        }
        StartCoroutine(PostLevelProcess.Instance.StartProcess());
    }
}
