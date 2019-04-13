using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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

    private bool ReloadNOW = false;
    public WeaponsSwitcher ws;

    int amountOfProjectiles = 1;

    public float fireRate = 0.5F;
    private float nextFire = 0.0F;
    public float AllowReload = 1.55f;

    void OnEnable()
    {
            Shell.SetActive(true);
    }
    void OnDisable()
    {
            Shell.SetActive(false);
    }

    void Update()
    {
            if (Input.GetButtonDown("Fire1") && Time.time > nextFire && shootpermission == true && Time.timeScale >= 0.5)
            {
                for (int i = 0; i < amountOfProjectiles; i++)
                {
                    Shoot();
                }
                nextFire = Time.time + fireRate;
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

            if (Input.GetButtonDown("Reload") && ammo <= 10)
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
        ReloadNOW = false;
        ammoTextActive.SetActive(true);
        ReloadImage.SetActive(false);
    }
    void ReloadSound()
    {
        StartCoroutine(ReloadFalse());
        ws.IsReloading = true;
        audioSource.PlayOneShot(Reload);
    }
    IEnumerator ReloadFalse()
    {
        yield return new WaitForSeconds(AllowReload);
        ws.IsReloading = false;
    }

}
