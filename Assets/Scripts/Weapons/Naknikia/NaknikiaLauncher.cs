using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class NaknikiaLauncher : MonoBehaviour
{
    [SerializeField]
    public float fireRate = 15f;
    public float ammo = 1;

    public GameObject LightBullet;

    public bool shootpermission = true;
    public Slider Slider;
    public GameObject SliderObject;
    public AudioClip Reload;
    public AudioSource audioSource;
    public Animator animator;
    public DropWeapon Dropweapon;
    public AudioClip shootSound;
    public GameObject AMMO;

    private float nextTimeToFire = 0f;

    public Transform WeaponParent;
    public Transform WeaponsHolder;

    public GameObject crosshair;

    public GameObject Naknik;
    public Transform shotPosition;

    void OnDisable()
    {
        LightBullet.SetActive(false);
        crosshair.SetActive(false);
        AMMO.SetActive(false);
        SliderObject.SetActive(false);
    }
    private void OnEnable()
    {
        shootpermission = true;
        Dropweapon.canDrop = true;
        StartCoroutine(CheckIfAmmoIsZero());
        AMMO.SetActive(false);
    }
    void FixedUpdate()
    {
        if (WeaponParent.IsChildOf(WeaponsHolder))
        {
            LightBullet.SetActive(true);
            crosshair.SetActive(true);
            SliderObject.SetActive(true);
            AMMO.SetActive(false);
            if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && shootpermission == true && Time.timeScale >= 0.5) //Mouse
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
            if (Input.GetAxis("RightTrigger") > 0f && Time.time >= nextTimeToFire && shootpermission == true && Time.timeScale >= 0.5) //Controller
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }

            if (Input.GetButtonDown("Reload") && ammo <= 59 && Time.timeScale >= 0.5)
            {
                animator.SetTrigger("Reload");
                shootpermission = false;
                Slider.value = 0;
                Dropweapon.canDrop = false;
            }
            if (ammo > 0)
            {
                Slider.value = 1;
            }
        }
        else
        {
            LightBullet.SetActive(false);
            crosshair.SetActive(false);
        }
    }
    void Shoot()
    {

        //Effect:
        //Audio:
        audioSource.PlayOneShot(shootSound);

        /* Ammo: */
        ammo -= 1;
        Instantiate(Naknik, shotPosition.position, shotPosition.rotation);
        StartCoroutine(CheckIfAmmoIsZero());
        Slider.value = 0;
    }
    IEnumerator CheckIfAmmoIsZero()
    {
        yield return new WaitForSeconds(0.5f);
        if (ammo <= 0 && shootpermission == true)
        {
            shootpermission = false;
            animator.SetTrigger("Reload");
            Dropweapon.canDrop = false;
        }
    }
    void ReloadAnimation()
    {
        ammo = 1f;
        shootpermission = true;
        Dropweapon.canDrop = true;
        Slider.value = 1;
    }
    void ReloadSound()
    {
        audioSource.PlayOneShot(Reload);
    }

    float timeToWait = 0.5f;
    float fillQuantity = 0.1f;
}
