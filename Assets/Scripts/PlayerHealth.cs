using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerHealth : MonoBehaviour
{
    public double currentHealth;
    public int maxHealth = 1000;
    public HealthBar healthBar;
    AudioSource damageSound;
    AudioSource[] sounds;

    void Start () {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        sounds = GameObject.FindGameObjectWithTag("Player").GetComponents<AudioSource>();
        Debug.Log("asd");
        damageSound = sounds[0];
    }

    public void TakeDamage(double damage)
	{
        Debug.Log(sounds);
        damageSound.Play();

        currentHealth -= damage;
        
		healthBar.SetHealth((int)currentHealth);

        if (currentHealth <= 0) { 
            Die(); 
        }
	}

    void Die () { 
        //Destroy(gameObject);
        FindObjectOfType<GameManager>().GameOver();
        FindObjectOfType<AudioManagerScript>().Play("game_over");
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
