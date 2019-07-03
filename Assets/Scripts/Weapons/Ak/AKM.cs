using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class AKM : MonoBehaviour
{
    [SerializeField]
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
    public AudioSource audioSource;
    public Animator animator;
    public DropWeapon Dropweapon;
    public AudioClip[] shootSounds;

    public Camera Cam;
    public ParticleSystem muzzleFlash;
    public GameObject ImpactEffect;

    private float nextTimeToFire = 0f;
    public float AllowReload = 2.25f;

    public Transform WeaponParent;
    public Transform WeaponsHolder;

    public GameObject crosshair;

    void OnDisable()
    {
        LightBullet.SetActive(false);
        crosshair.SetActive(false);

        ammoTextActive.SetActive(false);
        ReloadImage.SetActive(false);
    }
    private void OnEnable()
    {
        shootpermission = true;
        ammoTextActive.SetActive(true);
        ReloadImage.SetActive(false);
        Dropweapon.canDrop = true;
    }
    void FixedUpdate()
    {
        if (WeaponParent.IsChildOf(WeaponsHolder))
        {
            LightBullet.SetActive(true);
            crosshair.SetActive(true);
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && shootpermission == true && Time.timeScale >= 0.5) //Mouse
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
            if (Input.GetAxis("RightTrigger") > 0f && Time.time >= nextTimeToFire && shootpermission == true && Time.timeScale >= 0.5) //Controller
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
            if (ammo <= 0 && shootpermission == true)
            {
                shootpermission = false;
                animator.SetTrigger("Reload");
                ammoText.text = "0";
                ammoTextActive.SetActive(false);
                ReloadImage.SetActive(true);
                Dropweapon.canDrop = false;
            }

            if (Input.GetButtonDown("Reload") && ammo <= 59 && Time.timeScale >= 0.5)
            {
                animator.SetTrigger("Reload");
                shootpermission = false;
                ammoText.text = "0";
                ammoTextActive.SetActive(false);
                ReloadImage.SetActive(true);
                Dropweapon.canDrop = false;
            }
            if (ammo > 0)
            {
                ammoText.text = ammo.ToString();
            }
        }
        else{
            LightBullet.SetActive(false);
            crosshair.SetActive(false);
        }

    }
    public int DieHard = 0;
void Shoot()
    {
        //Effect:
        muzzleFlash.Play();
        //Audio:
        audioSource.PlayOneShot(shootSounds[Random.Range(0, shootSounds.Length)]);

        /* Ammo: */
        ammo -= 1;

        RaycastHit hit;
        if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out hit, range))
        {
            Enemy target = hit.transform.GetComponent<Enemy>();
            float damage = Random.Range(damageMin, damageMax);
            if (target != null)
            {
                target.TakeDamage(damage);
                if (target.health <= 0.9)
                {
                    DieHard = Random.Range(0, 3);
                    if (DieHard == 2)
                    {
                        target.DieHard();
                    }
                    else
                        return;
                }
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
        ammoTextActive.SetActive(true);
        ReloadImage.SetActive(false);
        Dropweapon.canDrop = true;

    }
    void ReloadSound()
    {
        audioSource.PlayOneShot(Reload);
    }
}
