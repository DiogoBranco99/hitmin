using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthBoostPickup : MonoBehaviour
{
    public GameObject pickupEffect;
    public GameObject doubleDamageUI;
    public GameObject cluesUI;
    public int duration;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Pickup());
        }
    }

    IEnumerator Pickup()
    {
        GameObject pickupGO = Instantiate(pickupEffect, transform.position, transform.rotation);
        Destroy(pickupGO, 2f);

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        doubleDamageUI.SetActive(false);
        cluesUI.SetActive(false);

        FindObjectOfType<PlayerHealth>().doubleHealth();

        yield return new WaitForSeconds(duration);

        doubleDamageUI.SetActive(true);
        cluesUI.SetActive(true);

        Destroy(gameObject);
    }
}
