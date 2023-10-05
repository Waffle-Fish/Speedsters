using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Master : MonoBehaviour
{
    public static Player1Master Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }
}
