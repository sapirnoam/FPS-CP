using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Naknikia : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody rb;
    public int damage = 30;
    private bool canDamage = true;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.forward * speed;
        InvokeRepeating("Selfdestroy", 10f, 10f);
        StartCoroutine(DisableDamageAfter());
    }

    void OnTriggerEnter(Collider Collision)
    {
        if (Collision.gameObject.tag == "Enemy" && canDamage == true)
        {
            Enemy enemy = Collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            StartCoroutine(DisableDamage());
        }
    }
    void Selfdestroy()
    {
        Destroy(gameObject, 0);
    }
    IEnumerator DisableDamage()
    {
        yield return new WaitForSeconds(0.01f);
        canDamage = false;
    }
    IEnumerator DisableDamageAfter()
    {
        yield return new WaitForSeconds(2f);
        canDamage = false;
    }
}
