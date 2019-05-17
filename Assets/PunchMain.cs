using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchMain : MonoBehaviour
{
    public Animator anim;

    public Transform WeaponParent;
    public Transform WeaponsHolder;

    public float fireRate = 0.0F;
    private float nextFire = 0.0F;

    public Camera Cam;

    public bool DamageAllowed = false;
    private bool IsPlaying = false;
    // Start is called before the first frame update
    // Update is called once per frame
    void FixedUpdate()
    {
        if (WeaponParent.IsChildOf(WeaponsHolder))
        {
            if (Input.GetButtonDown("Fire1") && Time.time > nextFire && Time.timeScale >= 0.5)
            {
                IsPlaying = true;
                Punch();
                nextFire = Time.time + fireRate;

            }
            if (Input.GetAxis("RightTrigger") > 0f && Time.time > nextFire && Time.timeScale >= 0.5)
            {
                DamageAllowed = true;
                Punch();
                nextFire = Time.time + fireRate;


            }
        }
        if (Input.GetButtonDown("Fire2"))
        {
            anim.SetBool("Dick", true);
        }
        if (Input.GetButtonUp("Fire2"))
        {
            anim.SetBool("Dick", false);
        }

    }
    void Punch()
    {
        if (null != anim)
        {
            int randomNumber = Random.Range(1, 5);
            anim.SetTrigger("Atc" + randomNumber);
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
}
