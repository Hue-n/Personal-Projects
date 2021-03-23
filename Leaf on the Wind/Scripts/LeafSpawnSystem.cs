using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LeafSystemStates
{ 
    disabled,
    enabled,
}

public class LeafSpawnSystem : MonoBehaviour
{
    public LeafSystemStates leafSystemState;
    public GameObject leaf;
    public Transform[] spawnPoints = new Transform[5];

    public float startTimeBtwSpawn;
    public float timeBtwSpawn;

    // Update is called once per frame
    void Update()
    {
        switch (leafSystemState)
        {
            case LeafSystemStates.disabled:
                break;
            case LeafSystemStates.enabled:
                TimeCheck();
                break;
        }
    }

    public void UpdateLeafSpawnState(LeafSystemStates newLeafSystemState)
    {
        leafSystemState = newLeafSystemState;
    }

    void TimeCheck()
    {
        if (timeBtwSpawn <= 0)
        {
            Spawn();
            timeBtwSpawn = Random.Range(5, 10);
        }

        else
        {
            timeBtwSpawn -= Time.deltaTime;
        }
    }

    void Spawn()
    {
        int i = Random.Range(0, 5);
        Instantiate(leaf, spawnPoints[i].position, Quaternion.identity);
    }
}
