using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShotGun : MonoBehaviour
{

    public float damage = 40;
    public float range = 45;
    public float ammo = 14;

    public GameObject Shell;


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


    int amountOfProjectiles = 2;

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
        if (Input.GetButtonDown("Fire1") && shootpermission == true && Time.timeScale >= 0.5)
        {
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
            audioSource.PlayOneShot(Reload);

        }

        if (Input.GetButtonDown("Reload") && ammo <= ammo)
        {
            ReloadNOW = true;
            animator.SetTrigger("Reload");
            shootpermission = false;
            ammoText.text = "0";
            ammoTextActive.SetActive(false);
            ReloadImage.SetActive(true);
            audioSource.PlayOneShot(Reload);
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
        ammo -= 1;

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
        animator2.SetTrigger("Reload");

    }

    void ReloadAnimation()
    {
        ammo = ammo;
        shootpermission = true;
        ReloadNOW = false;
        ammoTextActive.SetActive(true);
        ReloadImage.SetActive(false);
    }
}
