using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EggRainEffect : ItemCore
{
    [SerializeField]
    GameObject cloud;
    [SerializeField]
    GameObject eggs;
    [SerializeField]
    Sprite splatterdEgg;
    [SerializeField]
    [Tooltip("Amount of eggs that spawns within the effectDuration")]
    private int totalEggSpawn = 0;
    [SerializeField]
    private float cloudHeight = 0f;
    private List<GameObject> spawnList;

    private void Start()
    {
        Instantiate(cloud, new Vector3(transform.position.x, transform.position.y + cloudHeight, transform.position.z), Quaternion.identity, this.transform);
        //for (int i = 0; i < totalEggSpawn; i++)
        //{
        //    Instantiate(spawnList[i],cloud.transform.position,Quaternion.identity, this.transform);
        //    spawnList[i].SetActive(false);
        //}
        cloud.transform.position = new Vector3(transform.position.x, transform.position.y + cloudHeight, transform.position.z);
        cloud.SetActive(false);
    }

    public override IEnumerator ActivateEffect()
    {
        StopTimer();
        ResetTimer();
        StartTimer();
        int eggCounter = 1;
        SpawnCloud();
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

    private void SpawnCloud()
    {
        cloud.SetActive(true);
        // Do all the cloud animations here (?) 
    }
    private void OnDestroy()
    {
        //Destroy(cloud);
        //for (int i = 0; i < spawnList.Count; i++)
        //{
        //    Destroy(spawnList[i]);
        //}
    }
}
