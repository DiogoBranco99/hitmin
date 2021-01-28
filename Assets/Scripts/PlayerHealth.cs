using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth = 100;

    public HealthBar healthBar;

    void Start () {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update () { 
        if (Input.GetKeyDown(KeyCode.Tab)) {
			TakeDamage(20);
		}
    }

	void TakeDamage(int damage)
	{
		currentHealth -= damage;
        
		healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0) { 
            Die(); 
        }
	}

    void Die () { 
        Destroy(gameObject);
        FindObjectOfType<GameManager>().GameOver();
    }
}
