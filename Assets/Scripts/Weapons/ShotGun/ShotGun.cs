using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotGun : MonoBehaviour
{

    public float damageMin = 20;
    public float damageMax = 40;

    public float range = 45;
    public float ammo = 14;

    public GameObject Shell;


    public bool shootpermission = true;
    public Text ammoText;
    public GameObject ammoTextActive;
    public GameObject ReloadImage;
    public AudioClip Reload;
    public AudioClip MiniReload;
    public AudioSource audioSource;
    public Animator animator;
    public Animator animator2;

    public AudioClip[] shootSounds;

    public Camera Cam;
    public ParticleSystem muzzleFlash;
    public GameObject ImpactEffect;

    int amountOfProjectiles = 1;

    public float fireRate = 0.5F;
    private float nextFire = 0.0F;
    public float AllowReload = 1.55f;


    public Transform WeaponParent;
    public Transform WeaponsHolder;

    public GameObject crosshair;

    void OnDisable()
    {
        Shell.SetActive(false);
        crosshair.SetActive(false);

        ammoTextActive.SetActive(false);
        ReloadImage.SetActive(false);
    }
    private void OnEnable()
    {
        shootpermission = true;
        ammoTextActive.SetActive(true);
        ReloadImage.SetActive(false);
    }
    void FixedUpdate()
    {
        if (WeaponParent.IsChildOf(WeaponsHolder))
        {
            Shell.SetActive(true);
            crosshair.SetActive(true);
            if (Input.GetButtonDown("Fire1") && Time.time > nextFire && shootpermission == true && Time.timeScale >= 0.2) //Mouse
            {
                for (int i = 0; i < amountOfProjectiles; i++)
                {
                    Shoot();
                }
                nextFire = Time.time + fireRate;
            }
            if (Input.GetAxis("RightTrigger") > 0f && Time.time > nextFire && shootpermission == true && Time.timeScale >= 0.5) //Controller
            {
                for (int i = 0; i < amountOfProjectiles; i++)
                {
                    Shoot();
                }
                nextFire = Time.time + fireRate;
            }

            if (ammo <= 0 && shootpermission == true)
            {
                shootpermission = false;
                animator.SetTrigger("Reload");
                ammoText.text = "0";
                ammoTextActive.SetActive(false);
                ReloadImage.SetActive(true);
            }

            if (Input.GetButtonDown("Reload") && ammo <= 10 && Time.timeScale >= 0.5)
            {
                animator.SetTrigger("Reload");
                shootpermission = false;
                ammoText.text = "0";
                ammoTextActive.SetActive(false);
                ReloadImage.SetActive(true);
            }
            if (ammo > 0)
            {
                ammoText.text = ammo.ToString();
            }
        }
        else {
            Shell.SetActive(false);
            crosshair.SetActive(false);
        }
    }
    private int DieHard = 0;
    void Shoot()
    {
        muzzleFlash.Play();
        animator.SetTrigger("Pump");
        audioSource.PlayOneShot(shootSounds[Random.Range(0, shootSounds.Length)]);
        ammo -= 2;

        RaycastHit hit;
        if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out hit, range))
        {
            float damage = Random.Range(damageMin, damageMax);
            Enemy target = hit.transform.GetComponent<Enemy>();
            if (target != null)
            {
                target.TakeDamage(damage);
                if (target.health <= 0.9)
                {
                    DieHard = Random.Range(0, 2);
                    if (DieHard == 0)
                    {
                        return;
                    }
                    else if (DieHard == 1)
                    {
                        target.DieHard();
                    }
                }
            }
            if (hit.transform.tag != "Barrier" && hit.transform.tag != "MusicZone")
            {
                GameObject ImpactGO = Instantiate(ImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(ImpactGO, 2);
            }
        }
        animator2.SetTrigger("Reload");
        audioSource.PlayOneShot(MiniReload);
    }

    void ReloadAnimation()
    {
            ammo = 12;
            shootpermission = true;
            ammoTextActive.SetActive(true);
            ReloadImage.SetActive(false);
    }
    void ReloadSound()
    {
        audioSource.PlayOneShot(Reload);
    }

}
