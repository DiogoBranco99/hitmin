using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

    public float damage; 
    public float range; 
    public float fireRate;
    public float impactForce;

    public int maxAmmo;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;

    public Camera fpsCam; 
    public ParticleSystem muzzleFlash;
    // public GameObject impactEffect;

    private float nextTimeToFire = 0f;

    // public Animator animator;

    void Start() {
        currentAmmo = maxAmmo;
    }

    void OnEnable () {
        isReloading = false;
        // animator.SetBool("Reloading", false);
    }

    // Update is called once per frame 
    void Update () { 
        if(isReloading) 
            return; 

        if(currentAmmo <= 0) {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire) { 
            nextTimeToFire = Time.time + 1f/fireRate;
            Shoot(); 
        } 
    }

    IEnumerator Reload() {

        isReloading = true;
        Debug.Log("Reloading...");
        // animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime -.25f);
        // animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(.25f);
        currentAmmo = maxAmmo;
        isReloading = false;
    }

    void Shoot () { 

        muzzleFlash.Play();

        currentAmmo--;

        RaycastHit hit; 
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range)) { 
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();

            if(target != null) {
                target.TakeDamage(damage);
            }

            if(hit.rigidbody != null) {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
            
            // GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            // Destroy(impactGO, 2f);
        } 
    }
}