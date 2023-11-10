using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Settings", menuName = "Settings/SettingsScriptableObject", order = 1)]
public class SettingsScriptableObjects : ScriptableObject
{
    [Range(0f,1f)]
    public float MusicVolume = 0f;
    [Range(0f,1f)]
    public float SFXVolume = 0f;
}
