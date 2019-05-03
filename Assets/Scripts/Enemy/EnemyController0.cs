using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController0 : MonoBehaviour
{
    public float lookRadius = 30f;
    public float lookRadiusLock = 50;
    public float movementSpeed = 3;

    public bool LockedOnTarget = false;
    Transform Player;

    public AudioSource AudioS;
    public AudioClip[] Angry;
    private bool angryPlay = false;
    // Start is called before the first frame update
    void Start()
    {
        Player = PlayerManager.instance.player.transform;
        AudioS = GameObject.Find("Ambient Audio").GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(Player.position, transform.position);
        if (distance <= lookRadius)
        {
            if (angryPlay == false)
            {
                AudioS.PlayOneShot(Angry[Random.Range(0, Angry.Length)]);
                angryPlay = true;
            }
            transform.LookAt(Player.transform);
            transform.position += transform.forward * movementSpeed * Time.deltaTime;
            LockedOnTarget = true;

        }
        if (LockedOnTarget == true)
        {
            if (distance <= lookRadiusLock)
            {
                transform.LookAt(Player.transform);
                transform.position += transform.forward * movementSpeed * Time.deltaTime;
            }
        }
        if (distance >= lookRadiusLock)
        {
            LockedOnTarget = false;
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lookRadiusLock);
    }
}
