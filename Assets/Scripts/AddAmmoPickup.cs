﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAmmoPickup : MonoBehaviour
{
    public GameObject pickupEffect;
    public GameObject weaponHolder;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            GameObject pickupGO = Instantiate(pickupEffect, transform.position, transform.rotation);
            Destroy(pickupGO, 2f);

            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            weaponHolder.transform.GetChild(0).GetComponent<Gun>().addAmmo();
            weaponHolder.transform.GetChild(1).GetComponent<Gun>().addAmmo();


            Destroy(gameObject);
        }
    }

}