using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchMain : MonoBehaviour
{
    public Animator anim;

    public Transform WeaponParent;
    public Transform WeaponsHolder;

    public float fireRate = 0F;
    private float nextFire = 0.0F;

    public bool DamageAllowed = false;
    private bool IsPlaying = false;
    private bool SetTrigger = true;
    public Collider Hand1Collder;
    public Collider Hand2Collder;
    public Collider Hand11Collder;
    public Collider Hand22Collder;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        if (WeaponParent.IsChildOf(WeaponsHolder))
        {
            if (Input.GetButtonDown("Fire1") && Time.time > nextFire && Time.timeScale >= 0.5)
            {
                DamageAllowed = true;
                Punch();
                nextFire = Time.time + fireRate;
                SetTrigger = false;

            }
            if (Input.GetAxis("RightTrigger") > 0f && Time.time > nextFire && Time.timeScale >= 0.5)
            {
                DamageAllowed = true;
                Punch();
                nextFire = Time.time + fireRate;
            }
        }
        if (Input.GetButtonDown("Fire2") && Time.timeScale >= 0.5)
        {
            anim.SetBool("Dick", true);
        }
        if (Input.GetButtonUp("Fire2") && Time.timeScale >= 0.5)
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
        Hand1Collder.isTrigger = true;
        Hand11Collder.isTrigger = false;
        Hand22Collder.isTrigger = false;
        Hand2Collder.isTrigger = true;
    }


    public void DisableDamage()
    {
        DamageAllowed = false;
        IsPlaying = false;
        Hand11Collder.isTrigger = true;
        Hand22Collder.isTrigger = true;
        Hand1Collder.isTrigger = true;
        Hand2Collder.isTrigger = true;
    }
    private void OnEnable()
    {
        Hand1Collder.isTrigger = true;
        Hand11Collder.isTrigger = true;
        Hand22Collder.isTrigger = true;
        Hand2Collder.isTrigger = true;
    }
}
