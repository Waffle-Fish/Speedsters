using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomScreenUIManager : MonoBehaviour
{
    public static BottomScreenUIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }
}
