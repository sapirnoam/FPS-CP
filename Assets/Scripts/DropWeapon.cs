using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropWeapon : MonoBehaviour
{
    public Transform WeaponThis;
    public GameObject WeaponsHolderGameobject;
    public Rigidbody rb;
    public Collider collider;
    // Start is called before the first frame update
    void Start()
    {
        WeaponThis.GetComponent<Transform>();
        rb.GetComponent<Rigidbody>();
        collider.GetComponent<Collider>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Q)) //Should assign xbox controller here.
        {
            for (int i = 0; i < WeaponsHolderGameobject.transform.childCount; i++)
            {
                var child = WeaponsHolderGameobject.transform.GetChild(i).gameObject;
                if (child == null)
                    Debug.Log("");
                child.SetActive(true);
                break;
            }
            WeaponThis.parent = null;
            rb.isKinematic = false; //"disabling" the rigidbody (it's still active but gravity won't apply to it.
            rb.useGravity = true; //"disabling" the rigidbody (it's still active but gravity won't apply to it.
            collider.isTrigger = false; //disabling the collider.
            rb.AddForce(-3, 0, transform.forward.z * 500);
        }
    }
}
