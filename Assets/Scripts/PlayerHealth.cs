using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;

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
