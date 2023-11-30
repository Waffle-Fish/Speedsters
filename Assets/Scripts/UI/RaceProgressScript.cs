using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceProgressScript : MonoBehaviour
{
    [SerializeField]
    float startPoint;
    [SerializeField]
    float endPoint;

    [SerializeField]
    Slider slider;

    [SerializeField]
    GameObject player;

    private void Start()
    {
        slider.maxValue = endPoint;
        slider.minValue = startPoint;
    }
    // Update is called once per frame
    void Update()
    {
        slider.value = player.transform.position.x;
    }
}
