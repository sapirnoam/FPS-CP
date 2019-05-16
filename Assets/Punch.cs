using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    public Collider Hand;
    public int damage = 5;
    public PunchMain main;


    private void Start()
    {
        Hand.GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider Collision)
    {
        if (Collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("EnemyTouch");
            Enemy enemy = Collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (main.DamageAllowed == true)
                {
                    enemy.TakeDamage(damage);
                }
            }
        }
    }
}
