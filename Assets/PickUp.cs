using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{

    public GameObject playerCamera;
    public GameObject playerHand;

    float maxDistance = 10;
    public bool canPickUp;
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
        if (Physics.Raycast(r, out hitInfo))
        {
            //if it is tagged as a weapon
            if (hitInfo.transform.CompareTag("Weapon"))
            {
                print("We hit a weapon");
                //if the user presses F
                if (Input.GetKeyDown(KeyCode.F))
                {
                    print("We hit a weapon and user pressed F, we should pick up the weapon");
                    GameObject weapon = hitInfo.transform.gameObject;
                    weapon.gameObject.transform.parent = playerHand.transform;
                    weapon.gameObject.transform.localPosition = new Vector3(0, 0, 0);
                    weapon.gameObject.transform.localRotation = Quaternion.identity;
                    weapon.GetComponent<Rigidbody>().isKinematic = true; //"disabling" the rigidbody (it's still active but gravity won't apply to it.
                    weapon.GetComponent<Rigidbody>().useGravity = false; //"disabling" the rigidbody (it's still active but gravity won't apply to it.
                    weapon.GetComponent<Collider>().isTrigger = true; //disabling the collider.
                }
            }
        }
    }
}