using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

// TODO:
// Fix Egg Behavior Hitting Player
// Fix Destruction of Instantiated Eggs and Cloud
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
    private List<Sprite> splatterdEgg;
    [SerializeField]
    [Tooltip("Amount of eggs that spawns within the effectDuration")]
    private int totalEggSpawn = 0;
    [SerializeField]
    private float cloudHeight = 0f;
    [SerializeField][Min(0.01f)][Tooltip("How long it takes for an egg from cloud to player")]
    private float eggDropDuration = 0f;
    private List<GameObject> eggList = new();
    private int eggCounter = 0;

    private void Start()
    {
        SpawnEntities();
    }

    private void SpawnEntities()
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
        SpawnCloud();
        while (timer <= effectDuration)
        {
            cloud.GetComponentInParent<Transform>().position = new Vector2(enemy.transform.position.x, enemy.transform.position.y + cloudHeight);
            if (timer > (eggCounter + 1) * effectDuration / totalEggSpawn)
            {
                eggList[eggCounter].SetActive(true);
                StartCoroutine(RainEgg(eggList[eggCounter]));
                //UnityEngine.Debug.Log("Egg " + eggCounter + " spawned - Time: " + timer);
                eggCounter++;
            }
            timer += Time.deltaTime;
            yield return null;
        }
        ProcessEnd();
        //UnityEngine.Debug.Log("All the eggs have finished being spawned");

    }

    private IEnumerator RainEgg(GameObject egg)
    {
        if (egg == null) { UnityEngine.Debug.LogError("No Egg to drop"); }
        egg.transform.position = new(enemyTransform.position.x, enemyTransform.position.y + cloudHeight, enemyTransform.position.z);
        float spawnPosX = cloud.transform.position.x + UnityEngine.Random.Range(-1 * cloud.transform.localScale.x, cloud.transform.localScale.x);
        while (egg.transform.position.y > enemyTransform.position.y)
        {
            float dropRate = (cloudHeight / eggDropDuration) * Time.deltaTime;
            float spawnPosY = egg.transform.position.y - dropRate;
            Vector2 eggPos = new Vector2(spawnPosX, spawnPosY);
            egg.transform.position = eggPos;
            if (egg.GetComponent<EggBehavior>().HitPlayer)
            {
                ProcessHit();
                break;
            }
            yield return null;
        }
        egg.SetActive(false);
    }

    private void SpawnCloud()
    {
        cloud.transform.position = new(enemyTransform.position.x, enemyTransform.position.y + cloudHeight);
        cloud.SetActive(true);
        //UnityEngine.Debug.Log("Cloud has spawned");
    }

    private void ProcessHit()
    {
        UnityEngine.Debug.Log("Egg " + eggCounter + " has hit player " + enemy.tag);
        // Slow player

        // Create egg splatters
    }

    private void ProcessEnd()
    {
        StopTimer();
        ResetTimer();
        effectIsActive = false;
        cloud.SetActive(false);
    }

    private void OnDestroy()
    {
        Destroy(this);
    }
}
