using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDamagePickup : MonoBehaviour
{
    public GameObject pickupEffect;
    public GameObject weaponHolder;
    public GameObject healthBoostUI;
    public GameObject cluesUI;
    public int duration;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Pickup());
        }
        FindObjectOfType<AudioManagerScript>().Play("power_ups");
    }

    IEnumerator Pickup()
    {
        GameObject pickupGO = Instantiate(pickupEffect, transform.position, transform.rotation);
        Destroy(pickupGO, 2f);

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        weaponHolder.transform.GetChild(0).GetComponent<Gun>().doubleDamage();
        weaponHolder.transform.GetChild(1).GetComponent<Gun>().doubleDamage();

        healthBoostUI.SetActive(false);
        cluesUI.SetActive(false);

        yield return new WaitForSeconds(duration);

        weaponHolder.transform.GetChild(0).GetComponent<Gun>().resetDoubleDamage();
        weaponHolder.transform.GetChild(1).GetComponent<Gun>().resetDoubleDamage();

        healthBoostUI.SetActive(true);
        cluesUI.SetActive(true);

        Destroy(gameObject);
    }
}
