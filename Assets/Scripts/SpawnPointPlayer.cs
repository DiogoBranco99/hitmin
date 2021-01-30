using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointPlayer : MonoBehaviour
{
    public GameObject[] spawnLocations;
    public GameObject player;
/*    public Transform navmesh;
    public HealthBar healthbar;*/
    private Vector3 respawnLocation;

    void Awake()
    {
        spawnLocations = GameObject.FindGameObjectsWithTag("SpawnerPointPlayer");
    }

    // Start is called before the first frame update
    void Start()
    {
        /*       player = (GameObject)Resources.Load("Player", typeof(GameObject));
               navmesh = GameObject.FindGameObjectWithTag("NavmeshTag").transform;
               healthbar = FindObjectOfType<PlayerHealth>().healthBar;

               player.AddComponent<PlayerMovement>().groundCheck = navmesh;
               player.AddComponent<PlayerHealth>().healthBar = healthbar;*/


        player = (GameObject)GameObject.FindGameObjectWithTag("Player");
        
        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnPlayer()
    {
        int spawn = Random.Range(0, spawnLocations.Length);
        /*     GameObject.Instantiate(player, spawnLocations[spawn].transform.position, Quaternion.identity);
             Destroy(GameObject.FindGameObjectWithTag("Player"));*/
        player.transform.position = spawnLocations[spawn].transform.position;
    }
}