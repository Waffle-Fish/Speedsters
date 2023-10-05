using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Master : MonoBehaviour
{
    // This script acts as an easy way to reference player2
    public static Player2Master Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }
}
