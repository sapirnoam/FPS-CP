using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Gun : MonoBehaviour
{

    public float damageMin = 5;
    public float damageMax = 13;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;
    public float ammo = 60;

    public GameObject LightBullet;

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
    public WeaponsSwitcher ws;

    private bool ReloadNOW = false;
    private float nextTimeToFire = 0f;
    public float AllowReload = 1.30f;


    void OnEnable()
    {
        LightBullet.SetActive(true);
    }
    void OnDisable()
    {
        LightBullet.SetActive(false);
    }
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && shootpermission == true && Time.timeScale >= 0.5)
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
            float damage = Random.Range(damageMin, damageMax);
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
