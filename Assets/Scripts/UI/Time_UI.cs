using TMPro;
using UnityEngine;

public class Time_UI : MonoBehaviour
{
    public static Time_UI Instance { get; private set; }

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
    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }
    // Start is called before the first frame update
    void Start()
    {
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
            text1.text = DisplayTime(time);
            text2.text = text1.text;
        }
        else if (!isP1Finished && isP2Finished)
        {
            time += Time.deltaTime;
            finishFirst = '2';
            text1.text = DisplayTime(time);
        }
        else if (isP1Finished && !isP2Finished) 
        { 
            time += Time.deltaTime;
            finishFirst = '1';
            text2.text = DisplayTime(time);
        }
    }

    private string DisplayTime(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        float milli = time % 1 * 1000;

        return (string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milli));
    }

    public string GetPlayer1Time()
    {
        return text1.text;
    }

    public string GetPlayer2Time()
    {
        return text2.text;
    }

    public char GetWinner()
    {
        return finishFirst;
    }
}
