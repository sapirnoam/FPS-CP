using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBox : MonoBehaviour
{
    public GameObject Holder;
    public GameObject Box;
    public Animator anim;
    public Health health;
    public GameObject Effect;
    public Collider collider;
    private Rigidbody rb;

    public float HealthToGive = 30;
    private bool isOpening = false;
    private void Start()
    {
        Holder = transform.Find("Holder").gameObject;
        health = GameObject.Find("PlayerManager").GetComponent<Health>();
        rb.GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void LateUpdate()
    {
        //Opening Box from The Camera Pickup on the player. Key to open F, Long press. Reffrence to OpenBox();
    }

    public void OpenBox()
    {
        Debug.Log("OpenBox");
        anim.SetTrigger("Unlocking");
    }
    public void AddHealth()
    {
        if (isOpening == true)
        {
            HealthToGive = Random.Range(50f, 100f);
            health.health += 50;

            Instantiate(Effect, transform.position, transform.rotation);
            Destroy(gameObject);
            isOpening = false;
        }
    }
    public void Boolean()
    {
        isOpening = true;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            Destroy(Holder);
            rb.isKinematic = true;
            rb.useGravity = false;
            anim.SetTrigger("Grounded");
            Debug.Log("Ground");
        }
    }

}
