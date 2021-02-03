using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoostPickup : MonoBehaviour
{
    public GameObject pickupEffect;
    public int duration;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup();
        }
    }

    private void Pickup()
    {
        GameObject pickupGO = Instantiate(pickupEffect, transform.position, transform.rotation);
        Destroy(pickupGO, 2f);

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        FindObjectOfType<PlayerHealth>().doubleHealth();

        Destroy(gameObject);
    }
}
