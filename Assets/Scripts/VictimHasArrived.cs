using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictimHasArrived : MonoBehaviour
{

    void OnTriggerEnter(Collider c1)
    {
        if (c1.CompareTag("Victim"))
        {
            FindObjectOfType<GameManager>().LevelComplete();
        }
    }

}
