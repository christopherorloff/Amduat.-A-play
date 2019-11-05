using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlessedDead_Spawner_Hour8_Script : MonoBehaviour
{
    public Transform[] SpawnPoints;
    public float spawnTime = 30f;

    public GameObject blessedDead;

    void Start()
    {
        InvokeRepeating("SpawnTheObject", spawnTime, spawnTime);
    }

    void SpawnTheObject()
    {
        int spawnIndex = Random.Range(0, SpawnPoints.Length);


        Instantiate(blessedDead, SpawnPoints[spawnIndex].position, SpawnPoints[spawnIndex].rotation);
    }
}
