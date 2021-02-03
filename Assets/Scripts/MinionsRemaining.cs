using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MinionsRemaining : MonoBehaviour
{
    public GameObject[] minions;
    public TextMeshProUGUI minionsRemaining;

    void Update()
    {
        minions = GameObject.FindGameObjectsWithTag("Minion");
        if(minions.Length == 0) {
            minionsRemaining.color = Color.green;
            //FindObjectOfType<GameManager>().LevelComplete();
        }
        minionsRemaining.text = minions.Length.ToString("0");
    }

}
