using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Master : MonoBehaviour
{
    public static Player2Master Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }
}
