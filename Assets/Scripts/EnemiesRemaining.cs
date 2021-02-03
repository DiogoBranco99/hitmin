using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemiesRemaining : MonoBehaviour
{
    public GameObject[] minions;
    public GameObject[] hitmans;
    public TextMeshProUGUI enemiesRemaining;
    private int numberOfEnemies;

    void Update()
    {
        minions = GameObject.FindGameObjectsWithTag("Minion");
        hitmans = GameObject.FindGameObjectsWithTag("Enemy");
        numberOfEnemies = minions.Length + hitmans.Length;
        if (numberOfEnemies == 0) {
            enemiesRemaining.color = Color.green;
            Debug.Log("Inimigos mortos");
            FindObjectOfType<GameManager>().LevelComplete();
        }
        enemiesRemaining.text = numberOfEnemies.ToString("0");
    }

}
