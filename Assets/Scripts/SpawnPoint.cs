using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnPoint : MonoBehaviour
{
    private GameObject[] spawnLocations;
    private GameObject gObject;
    private GameObject[] gObjects;
    private string tagToFindObject;
    private string tagToFindLocations;
    private string parentCallingName;
    private bool findSingular = true;
    private bool isEnemyOrVictim = false;

    void Awake()
    {
        
        parentCallingName = gameObject.name;

        switch (parentCallingName){
            case "SpawnersPlayer":
                tagToFindLocations = "SpawnerPointPlayer";
                tagToFindObject = "Player";
                findSingular = true;
                isEnemyOrVictim = false;
                break;
            case "SpawnersEnemy":
                tagToFindLocations = "SpawnerPointEnemy";
                tagToFindObject = "Enemy";
                findSingular = true;
                isEnemyOrVictim = true;
                break;
            case "SpawnersPickups":
                tagToFindLocations = "SpawnerPointPickups";
                tagToFindObject = "Pickup";
                findSingular = false;
                isEnemyOrVictim = false;
                break;
            case "SpawnersVictim":
                tagToFindLocations = "SpawnerPointVictim";
                tagToFindObject = "Victim";
                findSingular = true;
                isEnemyOrVictim = true;
                break;
            case "SpawnersExchange":
                tagToFindLocations = "SpawnerPointExch";
                tagToFindObject = "ExchangePoint";
                findSingular = true;
                isEnemyOrVictim = false;
                break;
            default:
                break;
        }

        spawnLocations = GameObject.FindGameObjectsWithTag(tagToFindLocations);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (findSingular){
            gObject = (GameObject)GameObject.FindGameObjectWithTag(tagToFindObject);
            SpawnObj();
        } else {
            gObjects = GameObject.FindGameObjectsWithTag(tagToFindObject);
            SpawnObjects();
        }

    }

    private void SpawnObj()
    {
        int spawn = Random.Range(0, spawnLocations.Length);
        if(isEnemyOrVictim)
        {
            gObject.GetComponent<NavMeshAgent>().Warp(spawnLocations[spawn].transform.position);
        }
        else
        {
            gObject.transform.position = spawnLocations[spawn].transform.position;
        }
        
    }

    private void SpawnObjects()
    {
        int spawn;
        var locationToSpawn = new GameObject();
        for (int i = 0; i< gObjects.Length; i++){
            spawn = Random.Range(0, spawnLocations.Length);
            gObject = gObjects[i];
            locationToSpawn = spawnLocations[spawn];

            //transforms position
            gObject.transform.position = locationToSpawn.transform.position;

            //converts to list to remove index
            var tempList = toList(spawnLocations);
            tempList.Remove(locationToSpawn);
            spawnLocations = tempList.ToArray();
        }
    }

    private List<GameObject> toList(GameObject[] mArray)
    {
        var rList = new List<GameObject>();

        for(int i=0; i< mArray.Length; i++)
        {
            rList.Add(mArray[i]);
        }

        return rList;
    }
}