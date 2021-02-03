using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerHealth : MonoBehaviour
{
    public double currentHealth;
    public int maxHealth = 1000;
    public HealthBar healthBar;

    void Start () {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(double damage)
	{
		currentHealth -= damage;
        
		healthBar.SetHealth((int)currentHealth);

        if (currentHealth <= 0) { 
            Die(); 
        }
	}

    void Die () { 
        //Destroy(gameObject);
        FindObjectOfType<GameManager>().GameOver();
    }
}
