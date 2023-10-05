using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Master : MonoBehaviour
{
    // This script acts as an easy way to reference player1
    public static Player1Master Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }
}
