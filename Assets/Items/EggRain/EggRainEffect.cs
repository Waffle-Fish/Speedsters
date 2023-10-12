using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

// TODO:
// Have player detect being hit by egg
// Create Splatter effect on sceen
// Reduce player speed
// Change layer ordering of egg to cloud
// Change sprites

public class EggRainEffect : ItemCore
{
    [SerializeField]
    GameObject cloud;
    [SerializeField]
    GameObject egg;
    [SerializeField]
    Sprite splatterdEgg;
    [SerializeField]
    [Tooltip("Amount of eggs that spawns within the effectDuration")]
    private int totalEggSpawn = 0;
    [SerializeField]
    private float cloudHeight = 0f;
    [SerializeField][Min(0.01f)][Tooltip("How long it takes for an egg from cloud to player")]
    private float eggDropDuration = 0f;
    private List<GameObject> eggList = new();

    private void Start()
    {
        cloud = Instantiate(cloud, transform.position, cloud.transform.rotation, this.transform);
        for (int i = 0; i < totalEggSpawn; i++)
        {
            GameObject e = Instantiate(egg, cloud.transform.position, Quaternion.identity, transform);
            e.SetActive(false);
            eggList.Add(e);
        }
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
            cloud.GetComponentInParent<Transform>().position = new Vector2(enemy.transform.position.x, enemy.transform.position.y + cloudHeight);
            if (timer >= eggCounter * effectDuration / totalEggSpawn)
            {
                //Spawn egg
                UnityEngine.Debug.Log("Egg " + eggCounter + " spawned - Time: " + timer);
                eggList[eggCounter-1].SetActive(true);
                StartCoroutine(RainEgg(eggList[eggCounter - 1]));
                eggCounter++;
            }
            timer += Time.deltaTime;
            yield return null;
        }
        UnityEngine.Debug.Log("All the eggs have finished being spawned");
        StopTimer();
        ResetTimer();
        effectIsActive = false;
        cloud.SetActive(false);
    }

    private IEnumerator RainEgg(GameObject egg)
    {
        if (egg == null) { UnityEngine.Debug.LogError("No Egg to drop"); }
        egg.transform.position = new(enemyTransform.position.x, enemyTransform.position.y + cloudHeight, enemyTransform.position.z);
        float spawnPosX = cloud.transform.position.x + UnityEngine.Random.Range(-1 * cloud.transform.localScale.x, cloud.transform.localScale.x);
        while (egg.transform.position.y > enemyTransform.position.y)
        {
            float dropRate = (cloudHeight / eggDropDuration) * Time.deltaTime;
            UnityEngine.Debug.Log("DropRate: " + dropRate);
            float spawnPosY = egg.transform.position.y - dropRate;
            Vector2 eggPos = new Vector2(spawnPosX, spawnPosY);
            egg.transform.position = eggPos;
            yield return null;
        }
        egg.SetActive(false);
    }

    private void SpawnCloud()
    {
        cloud.transform.position = new(enemyTransform.position.x, enemyTransform.position.y + cloudHeight);
        cloud.SetActive(true);
        UnityEngine.Debug.Log("Cloud has spawned");
    }

    private void OnDestroy()
    {
        Destroy(cloud);
        for (int i = 0; i < eggList.Count; i++)
        {
            Destroy(eggList[i]);
        }
    }
}