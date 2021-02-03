using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CluePickup : MonoBehaviour
{
    public GameObject pickupEffect;
    public GameObject hotOrColdUI;
    public GameObject doubleDamageUI;
    public GameObject healthBoostUI;
    public int clueDuration;

    void OnTriggerEnter (Collider other) {
        if(other.CompareTag("Player")) {
            StartCoroutine(Pickup());
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

        yield return new WaitForSeconds(clueDuration);

        hotOrColdUI.SetActive(false);
        doubleDamageUI.SetActive(true);
        healthBoostUI.SetActive(true);

        Destroy(gameObject);
    }

    
}
