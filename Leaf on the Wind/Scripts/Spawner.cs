using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MasterSpawnerStates
{ 
    enabled,
    disabled
}
public class Spawner : MonoBehaviour
{
    [Header("Master Spawner")]
    public float startTimeBtwSpawn;
    public float timeBtwSpawn;
    public MasterSpawnerStates currentMasterSpawnerState;
    public GameObject[] obstacles = new GameObject[2];
    public Transform[] spawners = new Transform[2];

    public GameObject[] leaf = new GameObject[0];
    public Transform[] leafSpawners = new Transform[0];

    // Update is called once per frame
    void Update()
    {
        switch (currentMasterSpawnerState)
        {
            case (MasterSpawnerStates.disabled):
                break;
            case (MasterSpawnerStates.enabled):
                if (timeBtwSpawn <= 0)
                {
                    int i = Random.Range(0, 2);
                    Debug.Log(i);
                    Instantiate(obstacles[i], spawners[i].position, Quaternion.identity);
                    timeBtwSpawn = startTimeBtwSpawn;
                }

                else
                {
                    timeBtwSpawn -= Time.deltaTime;
                }
                break;
        }
    }

    public void UpdateMasterSpawnerState(MasterSpawnerStates newMasterSpawnerState)
    {
        currentMasterSpawnerState = newMasterSpawnerState;    
    }

    public void UpdateDifficulty(float newSpawnSpeed)
    {
        startTimeBtwSpawn = newSpawnSpeed;
    }

}
