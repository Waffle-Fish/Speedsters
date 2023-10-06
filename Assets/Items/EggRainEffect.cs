using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EggRainEffect : ItemCore
{
    [SerializeField]
    Sprite cloud;
    [SerializeField]
    Sprite eggs;
    [SerializeField]
    List<Sprite> splatterdEgg;
    [SerializeField]
    [Tooltip("Amount of eggs that spawns within the effectDuration")]
    int totalEggSpawn;

    public override IEnumerator ActivateEffect()
    {
        StopTimer();
        ResetTimer();
        StartTimer();
        int eggCounter = 1;
        while (timer <= effectDuration)
        {
            if(timer >= eggCounter * effectDuration / totalEggSpawn)
            {
                //Spawn egg
                UnityEngine.Debug.Log("Egg " + eggCounter + " spawned - Time: " + timer);
                eggCounter++;
            }
            timer += Time.deltaTime;
            yield return null;
        }
        UnityEngine.Debug.Log("All the eggs have finished being spawned");
        StopTimer();
        ResetTimer();
        effectIsActive = false;
    }
}
