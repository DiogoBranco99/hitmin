using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointEnemy : MonoBehaviour
{
    private GameObject[] spawnLocations;
    private GameObject enemy;

    void Awake()
    {
        spawnLocations = GameObject.FindGameObjectsWithTag("SpawnerPointEnemy");
    }

    // Start is called before the first frame update
    void Start()
    {
        enemy = (GameObject)GameObject.FindGameObjectWithTag("Enemy");

        Spawn();
    }

    private void Spawn()
    {
        int spawn = Random.Range(0, spawnLocations.Length);
        enemy.transform.position = spawnLocations[spawn].transform.position;
    }
}
