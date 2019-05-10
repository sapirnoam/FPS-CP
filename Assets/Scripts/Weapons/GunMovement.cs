using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController.Walkthrough.LandingLeavingGround;

public class GunMovement : MonoBehaviour
{
    public Transform Target;
    public Transform WeaponParent;
    public Transform WeaponsHolder;

    public Vector3 pickUpPosition;
    public Vector3 pickUpRotation;

    public void LateUpdate()
    {
        if (WeaponParent.IsChildOf(WeaponsHolder))
        {
            transform.LookAt(Target);
        }
        else
        {
            return;
        }
    }
}