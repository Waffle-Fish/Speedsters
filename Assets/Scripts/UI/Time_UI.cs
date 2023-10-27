using TMPro;
using UnityEngine;

public class Time_UI : MonoBehaviour
{
    public static Time_UI Instance;

    //Timer GameObjects
    [SerializeField]
    GameObject time_P1;
    [SerializeField]
    GameObject time_P2;

    float time = 0;
    public bool isP1Finished = false;
    public bool isP2Finished = false;

    private char finishFirst; 

    TextMeshProUGUI text1;
    TextMeshProUGUI text2;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        //Connect text
        text1 = time_P1.GetComponent<TextMeshProUGUI>();
        text2 = time_P2.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isP1Finished && !isP2Finished)
        {
            time += Time.deltaTime;
            text1.text = displayTime(time);
            text2.text = text1.text;
        }
        else if (!isP1Finished && isP2Finished)
        {
            time += Time.deltaTime;
            finishFirst = '2';
            text1.text = displayTime(time);
        }
        else if (isP1Finished && !isP2Finished) 
        { 
            time += Time.deltaTime;
            finishFirst = '1';
            text2.text = displayTime(time);
        }
    }

    private string displayTime(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        float milli = time % 1 * 1000;

        return (string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milli));
    }

    public string getPlayer1Time()
    {
        return text1.text;
    }

    public string getPlayer2Time()
    {
        return text2.text;
    }

    public char getWinner()
    {
        return finishFirst;
    }
}
