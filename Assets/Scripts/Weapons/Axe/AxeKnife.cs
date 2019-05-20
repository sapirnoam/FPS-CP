using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeKnife : MonoBehaviour
{
    public Collider Knife;
    public int damage = 5;
    public Axe main;

    void OnTriggerEnter(Collider Collision)
    {
        if (Collision.gameObject.CompareTag("Enemy"))
        {
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
