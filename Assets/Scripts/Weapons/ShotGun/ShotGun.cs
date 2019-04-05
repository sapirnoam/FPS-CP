using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShotGun : MonoBehaviour
{

    public float damage = 35;
    public float range = 45;
    public float ammo = 12;


    public bool shootpermission = true;
    public Text ammoText;
    public GameObject ammoTextActive;
    public GameObject ReloadImage;
    public AudioClip Reload;
    public AudioSource audioSource;
    public Animator animator;
    public Animator animator2;
    public AudioClip[] shootSounds;

    public Camera Cam;
    public ParticleSystem muzzleFlash;
    public GameObject ImpactEffect;

    private bool ReloadNOW = false;

    public float fireRate = 1F;
    private float nextFire = 0.0F;

    int amountOfProjectiles = 2;
    void Update()
    {
        if (Time.time > nextFire && Input.GetButtonDown("Fire1") && shootpermission == true)
        {
            nextFire = Time.time + fireRate;
            for (int i = 0; i < amountOfProjectiles; i++)
            {
                Shoot();
            }
        }



        if (ammo <= 0 && shootpermission == true)
        {
            ReloadNOW = true;
            shootpermission = false;
            animator.SetTrigger("Reload");
            ammoText.text = "0";
            ammoTextActive.SetActive(false);
            ReloadImage.SetActive(true);
        }

        if (Input.GetButtonDown("Reload") && ammo <= 12)
        {
            ReloadNOW = true;
            animator.SetTrigger("Reload");
            shootpermission = false;
            ammoText.text = "0";
            ammoTextActive.SetActive(false);
            ReloadImage.SetActive(true);
        }
        if (ammo > 0 && ReloadNOW == false)
        {
            ammoText.text = ammo.ToString();
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();
        audioSource.PlayOneShot(shootSounds[Random.Range(0, shootSounds.Length)]);
        ammo -= 2;

        RaycastHit hit;
        if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out hit, range))
        {
            Enemy target = hit.transform.GetComponent<Enemy>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
            if (hit.transform.tag != "Barrier" && hit.transform.tag != "MusicZone")
            {
                GameObject ImpactGO = Instantiate(ImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(ImpactGO, 2);
            }
        }
    }


    void ReloadAnimation()
    {
        ammo = 12;
        shootpermission = true;
        ReloadNOW = false;
        ammoTextActive.SetActive(true);
        ReloadImage.SetActive(false);
    }
    void ReloadSound()
    {
        audioSource.PlayOneShot(Reload);
    }
}
