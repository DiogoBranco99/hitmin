using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAmmoPickup : MonoBehaviour
{
    public GameObject pickupEffect;
    public GameObject weaponHolder;
    public GameObject healthBoostUI;
    public GameObject cluesUI;
    private GameObject[] healthBoost;
    private GameObject[] clues;
    private GameObject[] addAmmo;
    public int duration;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            healthBoost = GameObject.FindGameObjectsWithTag("HealthBoosts");
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

        weaponHolder.transform.GetChild(0).GetComponent<Gun>().addAmmo();
        weaponHolder.transform.GetChild(1).GetComponent<Gun>().addAmmo();

        healthBoostUI.SetActive(false);
        cluesUI.SetActive(false);

        disablePickups();

        yield return new WaitForSeconds(duration);
        
        healthBoostUI.SetActive(true);
        cluesUI.SetActive(true);

        resetPickups();

        Destroy(gameObject);
    }

    void disablePickups()
    {
        for (int i = 0; i < clues.Length; i++)
        {
            clues[i].SetActive(false);
        }
        for (int i = 0; i < healthBoost.Length; i++)
        {
            healthBoost[i].SetActive(false);
        }
    }

    void resetPickups()
    {
        for (int i = 0; i < clues.Length; i++)
        {
            clues[i].SetActive(true);
        }
        for (int i = 0; i < healthBoost.Length; i++)
        {
            healthBoost[i].SetActive(true);
        }
    }

}