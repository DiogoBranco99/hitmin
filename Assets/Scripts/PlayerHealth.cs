using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerHealth : MonoBehaviour
{
    public Transform enemy;
    public int currentHealth;
    public int maxHealth = 1000;
    public LayerMask whatIsPlayer;

    public HealthBar healthBar;

    void Start () {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        enemy =GameObject.Find("Enemy").transform;
    }

    void Update () { 
        if(enemy == null)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Tab)) {
			TakeDamage(20);
		}
        if (inDistance())
        {
            TakeDamage(1);
        }
    }

    bool inDistance()
    {
        float aRange = 5;
        return Physics.CheckSphere(enemy.position, aRange, whatIsPlayer);
        
    }

	public void TakeDamage(int damage)
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
