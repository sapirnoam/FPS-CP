using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulle : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody rb;
    public int damage = 40;
    public float SelfDestroy = 1.5f;
    public bool Destroy = false;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        Invoke("Selfdestroy", SelfDestroy);
    }

    void OnTriggerEnter(Collider Collision)
    {
        if (Collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = Collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject, 0);
        }
    //Destroy(GameObject); 
    }
    void Selfdestroy()
    {
        if (Destroy == true)
        {
        Destroy(gameObject, 0);
        }
    }


}
