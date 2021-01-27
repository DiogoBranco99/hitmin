using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemiesRemaining : MonoBehaviour
{
    public GameObject[] enemies;
    public TextMeshProUGUI enemiesRemaining;

    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemies.Length == 0) {
            enemiesRemaining.color = Color.green;
            FindObjectOfType<GameManager>().LevelComplete();
        }
        enemiesRemaining.text = enemies.Length.ToString("0");
    }

}
