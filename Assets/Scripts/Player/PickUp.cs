using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{

    public GameObject playerCamera;
    public GameObject playerHand;

    public float maxDistance = 5;
    public GameObject PickUpText;
    void Start()
    {
        PickUpText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitInfo;
        Ray r = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        //if we hit something
        if (Physics.Raycast(r, out hitInfo, maxDistance))
        {
            //if it is tagged as a weapon
            if (hitInfo.transform.CompareTag("Weapon") && hitInfo.transform.parent == null)
            {
                PickUpText.SetActive(true);
                //if the user presses F
                if (Input.GetButtonDown("PickUp")) //Should assign xbox controller here.
                {
                    for (int i = 0; i < playerHand.transform.childCount; i++)
                    {
                        var child = playerHand.transform.GetChild(i).gameObject;
                        if (child != null)
                            child.SetActive(false);
                    }
                    GameObject weapon = hitInfo.transform.gameObject;
                    weapon.gameObject.transform.parent = playerHand.transform;
                    weapon.gameObject.transform.localPosition = new Vector3(-0.1584091f, 0f, 0.09350014f);
                    weapon.gameObject.transform.localRotation = Quaternion.identity;
                    weapon.GetComponent<Rigidbody>().isKinematic = true; //"disabling" the rigidbody (it's still active but gravity won't apply to it.
                    weapon.GetComponent<Rigidbody>().useGravity = false; //"disabling" the rigidbody (it's still active but gravity won't apply to it.
                    weapon.GetComponent<Collider>().isTrigger = true; //disabling the collider.
                    PickUpText.SetActive(false);
                }
            }
            else {
                PickUpText.SetActive(false);
            }
        }
    }
}