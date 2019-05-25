using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController.Walkthrough.LandingLeavingGround;

public class GunMovement : MonoBehaviour
{
    public Transform Target;
    public Transform WeaponThis;
    public Transform WeaponsHolder;

    void Start()
    {
        Target.GetComponent <Transform>();
        WeaponThis.GetComponent<Transform>();
    }

    public void Update()
    {
        if (WeaponThis.IsChildOf(WeaponsHolder))
        {
            transform.LookAt(Target);
        }
        else
        {
            return;
        }
    }
}