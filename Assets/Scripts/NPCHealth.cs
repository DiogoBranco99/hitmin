using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCHealth : MonoBehaviour
{
    public double currentHealth;
    public int maxHealth = 1000;
    public HealthBar healthBar;
    AudioSource damageSound;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        damageSound = GetComponent<AudioSource>();
    }

    public void TakeDamage(double damage)
    {
        damageSound.Play();

        currentHealth -= damage;

        healthBar.SetHealth((int)currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //Destroy(gameObject);
        FindObjectOfType<GameManager>().GameOver();
        
    }

    public void doubleHealth()
    {
        currentHealth *= 2;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBar.SetHealth((int)currentHealth);
    }
}
