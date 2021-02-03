using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerHealth : MonoBehaviour
{
    public Transform enemy;
    public double currentHealth;
    public int maxHealth = 1000;
    public LayerMask whatIsPlayer;
    int minionInDistance;
    string minion = "Minion";
    List<Transform> minions;
    List<Transform> minionsInAttack;
    public HealthBar healthBar;

    void Start () {
     
        minions = new List<Transform>();
        minionsInAttack = new List<Transform>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        enemy =GameObject.Find("Enemy").transform;
    }

    void Update () {
        int multiplier = 0;
        double damage = 0.3;
        if(enemy != null)
        {
            if (inDistance(enemy))
            {
                TakeDamage(1);
            }
        }
        if (Input.GetKeyDown(KeyCode.Tab)) {
			TakeDamage(20);
		}
        multiplier = RefreshMinionCount();
        TakeDamage(damage * multiplier);
    }

    bool inDistance(Transform t)
    {
        float aRange = 5;
        return Physics.CheckSphere(t.position, aRange, whatIsPlayer);
    }


    int RefreshMinionCount()
    {
        int max = 3;
        int i = 1;
        int ret = 0;
        string minion = "Minion";
        while (i <= max)
        {
            string nameofminion = minion + i.ToString();
            if (GameObject.Find(nameofminion).transform != null)
            {
                if (inDistance(GameObject.Find(nameofminion).transform))
                {
                    ret++;
                }
                i++;
            }
        }
        return ret;
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
        Destroy(gameObject);
        FindObjectOfType<GameManager>().GameOver();
    }
}
