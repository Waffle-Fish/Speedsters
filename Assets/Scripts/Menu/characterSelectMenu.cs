using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectMenu : MonoBehaviour
{
    public static CharacterSelectMenu Instance { get; private set; }

    //[Min(0)]
    public int sceneIndex {get; private set;}

    [SerializeField]
    bool isPlayer2;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }

    public void PlayGame()
    {
        Debug.Log("Lets play!");
        if (sceneIndex >= SceneManager.sceneCountInBuildSettings) { Debug.LogError("Trying to load a scene outside the current number of scenes in the build settings"); }
        SceneManager.LoadScene(sceneIndex);
    }

    //Function used in LevelSelect (Reference to the MainMenu script should only be made form characterSelects script)
    public void SetIndex(int index)
    {
        sceneIndex = index;
    }

    //Checks if both players are ready if 2P mode, otherwise, checks if player is ready
    public void checkPlayerReady()
    {
        if (isPlayer2)
        {
            if (ReadyButton.Instance.p1ready && ReadyButton.Instance.p2ready)
            {
                PlayGame();
            } 
        }
        else
        {
            if (ReadyButton.Instance.p1ready) 
            { 
                PlayGame(); 
            }
        }
    }

    public void setIsPlayer2(bool state)
    {
        isPlayer2 = state;
    }

    public bool getIsPlayer2()
    {
        return isPlayer2;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}