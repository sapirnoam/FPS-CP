using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggersEnter : MonoBehaviour
{
    public Health health;
    private float nextTimeToFire = 0f;
    public float fireRate = 10f;

    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Lava" && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            health.health -= 35f;
            //Play Sound.
            //Change material
        }
        //if (other.gameObject.tag == "HotLava" && Time.time >= nextTimeToFire)
        //{
        //    nextTimeToFire = Time.time + 1f / fireRate;
         //   health.health -= 5f;
         //   Instantiate(effectSmoke, transform.position, transform.rotation);
         //   //Play Sound.
         //   //Change 
       // }
    }
}
