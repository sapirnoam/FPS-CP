using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultWeapon : MonoBehaviour
{
    public GameObject noWeapon;
    public GameObject Ammo;

    void OnEnable()
    {
        noWeapon.SetActive(true);
        Ammo.SetActive(false);

    }

    void OnDisable()
    {
        noWeapon.SetActive(false);
        Ammo.SetActive(true);
    }
}
