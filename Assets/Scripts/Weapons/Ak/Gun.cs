﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Gun : MonoBehaviour
{

    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;
    public float ammo = 60;


    public bool shootpermission = true;
    public Text ammoText;
    public GameObject ammoTextActive;
    public GameObject ReloadImage;
    public AudioClip Reload;
    public AudioClip Fire;
    public AudioSource audioSource;
    public Animator animator;
    public AudioClip[] shootSounds;

    public Camera Cam;
    public ParticleSystem muzzleFlash;
    public GameObject ImpactEffect;

    private bool ReloadNOW = false;
    private float nextTimeToFire = 0f;
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && shootpermission == true)
        {
          nextTimeToFire = Time.time + 1f / fireRate;
          Shoot();
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

        if (Input.GetButtonDown("Reload") && ammo <= 59)
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
        //Effect:
        muzzleFlash.Play();

        //Audio:
        audioSource.PlayOneShot(shootSounds[Random.Range(0, shootSounds.Length)]);


        /* Ammo: */ ammo -= 1;

        RaycastHit hit;
        if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out hit, range))
        {
            Enemy target = hit.transform.GetComponent<Enemy>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out hit, range))
            {
                if (hit.transform.tag != "Barrier" && hit.transform.tag != "MusicZone")
                {
                    GameObject ImpactGO = Instantiate(ImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(ImpactGO, 2);
                }
            }
        }
    }

    void ReloadAnimation()
    {
        ammo = 60;
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
