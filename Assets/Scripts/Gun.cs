using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class Gun : MonoBehaviour
{

    public TextMeshProUGUI ammoDisplay;

    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;

    public int maxAmmo = 10;
    private int currentAmmo;
    private int ammoToReload;
    public int ammoToAdd;
    public float reloadTime = 1f;
    private bool isReloading = false;
    private GameObject player;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public GameObject bloodSplatter;
    public GameObject lightningEffect;
    private GameObject projectile;

    private float nextTimeToFire = 0f;

    public bool weaponStuns;

    AudioSource shootingSound;
    AudioSource noBullets;
    AudioSource reloadSound;

    public Animator animator;

    void Start()
    {
        currentAmmo = maxAmmo;
        player = GameObject.FindGameObjectWithTag("Player");
        projectile = GameObject.Find("projectile");
        shootingSound = GetComponents<AudioSource>()[0];
        reloadSound = GetComponents<AudioSource>()[1];
        noBullets = GetComponents<AudioSource>()[2];
    }

    void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }

    // Update is called once per frame 
    void Update()
    {
        ammoDisplay.text = "Ammo: " + currentAmmo.ToString() + "/" + ammoToReload.ToString();

        if (isReloading)
            return;

        if (currentAmmo <= 0 && ammoToReload > 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && currentAmmo <= 0)
        {
            noBullets.Play();
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && currentAmmo > 0)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
            sendBall();
            shootingSound.Play();
        }
    }

    void sendBall()
    {
        GameObject go = (GameObject)Instantiate(projectile, player.transform.position + player.transform.forward * 2, player.transform.rotation);
        go.GetComponent<Rigidbody>().AddForce(player.transform.forward * 1400f);
        Destroy(go, 1f);
    }

    IEnumerator Reload()
    {

        isReloading = true;
        animator.SetBool("Reloading", true);
        reloadSound.Play();
        yield return new WaitForSeconds(reloadTime - .5f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(.5f);
        if (ammoToReload <= maxAmmo)
        {
            currentAmmo = ammoToReload;
        }
        else
        {
            currentAmmo = maxAmmo;
        }
        ammoToReload -= currentAmmo;
        isReloading = false;
    }

    void Shoot()
    {

        muzzleFlash.Play();

        currentAmmo--;

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {

            MinionAI target1 = hit.transform.GetComponent<MinionAI>();
            EnemyAI target2 = hit.transform.GetComponent<EnemyAI>();

            if (target1 != null || target2 != null)
            {
                if (target1 != null)
                {
                    GameObject bloodGO = Instantiate(bloodSplatter, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(bloodGO, 2f);
                    target1.TakeDamage(damage);
                }
                if (target2 != null)
                {
                    GameObject lightningGO = Instantiate(lightningEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(lightningGO, 2f);
                    target2.StunOrSlow(weaponStuns);
                }
            }

            else
            {
                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2f);
            }

            if (hit.rigidbody != null)
            {
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

    public void addAmmo()
    {
        ammoToReload += ammoToAdd; //podemos decidir depois o valor
    }

}