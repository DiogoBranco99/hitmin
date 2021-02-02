using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CluePickup : MonoBehaviour
{
    public GameObject pickupEffect;
    public GameObject hotOrColdUI;
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

        yield return new WaitForSeconds(clueDuration);

        hotOrColdUI.SetActive(false);

        Destroy(gameObject);
    }

    
}
