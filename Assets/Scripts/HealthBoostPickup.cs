using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthBoostPickup : MonoBehaviour
{
    public GameObject pickupEffect;
    public GameObject doubleDamageUI;
    public GameObject cluesUI;
    private GameObject[] clues;
    private GameObject[] addAmmo;
    private GameObject[] healthBoost;
    public int duration;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            addAmmo = GameObject.FindGameObjectsWithTag("AddAmmo");
            clues = GameObject.FindGameObjectsWithTag("Clues");
            StartCoroutine(Pickup());
            FindObjectOfType<AudioManagerScript>().Play("power_ups");
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

        disablePickups();

        FindObjectOfType<NPCHealth>().doubleHealth();

        yield return new WaitForSeconds(duration);

        doubleDamageUI.SetActive(true);
        cluesUI.SetActive(true);

        resetPickups();

        Destroy(gameObject);
    }

    void disablePickups()
    {
        for (int i = 0; i < addAmmo.Length; i++)
        {
            addAmmo[i].SetActive(false);
        }
        for (int i = 0; i < clues.Length; i++)
        {
            clues[i].SetActive(false);
        }

    }

    void resetPickups()
    {
        for (int i = 0; i < addAmmo.Length; i++)
        {
            addAmmo[i].SetActive(true);
        }
        for (int i = 0; i < clues.Length; i++)
        {
            clues[i].SetActive(true);
        }

    }
}
