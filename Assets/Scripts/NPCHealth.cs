using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHealth : MonoBehaviour
{
    public float health;

    void Start () {
        health = 50f;
    }
    
    void Update () { 
        if (health <= 0f) { 
            Die(); 
        } 
    }


    void Die () { 
        Destroy(gameObject);
        FindObjectOfType<GameManager>().GameOver();
    }
}
