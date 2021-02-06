using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CluePickup : MonoBehaviour
{
    public GameObject pickupEffect;
    public GameObject hotOrColdUI;
    public GameObject doubleDamageUI;
    public GameObject healthBoostUI;
    private GameObject[] healthBoost;
    private GameObject[] addAmmo;
    private GameObject[] clues;
    public int clueDuration;

    void OnTriggerEnter (Collider other) {
        if(other.CompareTag("Player")) {
            addAmmo = GameObject.FindGameObjectsWithTag("AddAmmo");
            healthBoost = GameObject.FindGameObjectsWithTag("HealthBoosts");
            clues = GameObject.FindGameObjectsWithTag("Clues");
            StartCoroutine(Pickup());
            FindObjectOfType<AudioManagerScript>().Play("power_ups");
        }
    }

    IEnumerator Pickup() {
        GameObject pickupGO = Instantiate (pickupEffect, transform.position, transform.rotation);
        Destroy(pickupGO, 2f);

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        hotOrColdUI.SetActive(true);
        doubleDamageUI.SetActive(false);
        healthBoostUI.SetActive(false);

        disablePickups();

        yield return new WaitForSeconds(clueDuration);

        hotOrColdUI.SetActive(false);
        doubleDamageUI.SetActive(true);
        healthBoostUI.SetActive(true);

        resetPickups();

        Destroy(gameObject);
    }

    void disablePickups()
    {
        for (int i = 0; i < addAmmo.Length; i++)
        {
            addAmmo[i].SetActive(false);
        }
        for (int i = 0; i < healthBoost.Length; i++)
        {
            healthBoost[i].SetActive(false);
        }
    }

    void resetPickups()
    {
        for (int i = 0; i < addAmmo.Length; i++)
        {
            addAmmo[i].SetActive(true);
        }
        for (int i = 0; i < healthBoost.Length; i++)
        {
            healthBoost[i].SetActive(true);
        }
    }
}
