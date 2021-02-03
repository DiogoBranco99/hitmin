using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

    public float damage = 10f; 
    public float range = 100f; 
    public float fireRate = 15f;
    public float impactForce = 30f;

    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;

    public Camera fpsCam; 
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public GameObject bloodSplatter;

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

            MinionAI target1 = hit.transform.GetComponent<MinionAI>();
            EnemyAI target2 = hit.transform.GetComponent<EnemyAI>();

            if (target1 != null || target2 != null) {
                if(target1 != null)
                {
                    GameObject bloodGO = Instantiate(bloodSplatter, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(bloodGO, 2f);
                    target1.TakeDamage(damage);
                }
                if (target2 != null)
                {
                    GameObject bloodGO = Instantiate(bloodSplatter, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(bloodGO, 2f);
                    target2.TakeDamage(damage);
                }
            }

            else
            {
                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2f);
            }

            if(hit.rigidbody != null) {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
            
        } 
    }

    public void doubleDamage()
    {
        damage *= 2;
    }

    public void resetDoubleDamage()
    {
        damage /= 2;
    }
}