using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    public float GeneratorHealth = 500;
    public float GizmoSize = 50;

    public GameObject Enemy;
    public Transform[] SpawnLocations;

    Transform Player;

    public void Start()
    {
        Player = PlayerManager.instance.player.transform;
    }

    void Update()
    {
        float distance = Vector3.Distance(Player.position, transform.position);
        if (distance <= GizmoSize)
        {
            
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, GizmoSize);
    }
}
