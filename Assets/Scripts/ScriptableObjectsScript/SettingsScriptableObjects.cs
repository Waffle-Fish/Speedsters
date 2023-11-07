using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Assets", order = 1)]
public class SettingsScriptableObjects : ScriptableObject
{
    public float MusicVolume;
    public float SFXVolume;
}
