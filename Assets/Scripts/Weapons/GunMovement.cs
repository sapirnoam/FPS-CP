using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController.Walkthrough.LandingLeavingGround;

public class GunMovement : MonoBehaviour
{
    public Transform Target;
    public void LateUpdate()
    {
            transform.LookAt(Target);
    }
}