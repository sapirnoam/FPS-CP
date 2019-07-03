using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    public Animator anim;

    public Transform WeaponParent;
    public Transform WeaponsHolder;

    public float fireRate = 1.03F;
    private float nextFire = 0.0F;

    public bool DamageAllowed = false;
    private bool IsPlaying = false;
    public MeshCollider meshCollider;
    public DropWeapon Dropweapon;

    public GameObject WeaponIcon;
    public AudioClip[] HoverSounds;
    public AudioSource audios;
    // Start is called before the first frame update
    // Update is called once per frame
    void FixedUpdate()
    {
        if (WeaponParent.IsChildOf(WeaponsHolder))
        {
            meshCollider.isTrigger = true;
            if (Input.GetButtonDown("Fire1") && Time.time > nextFire && Time.timeScale >= 0.5)
            {
                DamageAllowed = true;
                Hover();
                Dropweapon.canDrop = false;
                nextFire = Time.time + Time.deltaTime * fireRate;

            }
            if (Input.GetAxis("RightTrigger") > 0f && Time.time > nextFire && Time.timeScale >= 0.5)
            {
                DamageAllowed = true;
                Hover();
                Dropweapon.canDrop = false;
                nextFire = Time.time + fireRate;
            }
        }
        else
        {
            meshCollider.isTrigger = false;
            WeaponIcon.SetActive(false);
        }
    }
    void Hover()
    {
        if (null != anim)
        {
            int randomNumber = Random.Range(1, 4);
            anim.SetTrigger("Atk" + randomNumber);
            audios.PlayOneShot(HoverSounds[Random.Range(1, HoverSounds.Length)]);
        }
    }

    public void AllowDamage()
    {
        DamageAllowed = true;
    }


    public void DisableDamage()
    {
        DamageAllowed = false;
        IsPlaying = false;
    }
    public void CanDrop()
    {
        Dropweapon.canDrop = true;
    }
}
