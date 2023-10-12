using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopScreenUIManager : MonoBehaviour
{
    public static TopScreenUIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }
}
