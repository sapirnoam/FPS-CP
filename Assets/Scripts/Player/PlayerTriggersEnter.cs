using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggersEnter : MonoBehaviour
{
    public Health health;
    private float nextTimeToFire = 0f;
    public float fireRate = 15f;
    public GameObject effectFire; // The death Fire effect
    public GameObject effectSmoke; // The death Fire effect

    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Lava" && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            health.health -= 30f;
            //Play Sound.
            //Change material
            Instantiate(effectFire, transform.position, transform.rotation);

        }
        if (other.gameObject.tag == "HotLava" && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            health.health -= 5f;
            Instantiate(effectSmoke, transform.position, transform.rotation);
            //Play Sound.
            //Change 
        }
    }
}
