using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 50f; // Enemy health.
    public float GiveHealth = 6;
    public float ScoreAdded = 10; // The money value that should be added after enemy death.
    public Score scoreScript; // Get the Money information.


    //Materials / Damage//
    public Material MaterialEyesurfesDamaged;//material eye
    public Material MaterialEyesurfesDamagedHARD; //material eye

    public GameObject EyeSurfes; //object
    public GameObject Body; //object

    public float HealthToShowDamage = 30; //floats
    public float HealthToShowHardDamage = 10; //floats

    //GORE//
    public Transform effecttransform; // The transform for the effect
    public AudioClip[] AudioImpact; //Impact audio
    public AudioClip deathAudio;
    public GameObject effect; // The death effect


    // Attacking the player components ; Attacking while player tuches the enemy!, Not weapon attacking.//


    public float Damage = 20; // Damage x to the player
    Transform Player; // Get Player Transform for the gizmo
    public Health healthPlayer; // Player Health script
    public float AttackRadious = 10; // How far the enemy hit

    // Audio //
    public AudioSource AudioS;

    // Getting components.
    void Start()
    {
        effecttransform = this.gameObject.transform.GetChild(4);
        AudioS = FindObjectOfType<AudioSource>();
        scoreScript = FindObjectOfType<Score>(); 
        healthPlayer = FindObjectOfType<Health>();
        Player = PlayerManager.instance.player.transform;
    }

    // Damage to the Enemy.
    public void TakeDamage(float amount)
    {
        health -= amount;
        AudioS.PlayOneShot(AudioImpact[Random.Range(0, AudioImpact.Length)]);
        if (health <= 0f)
        {
            Die();
            AudioS.PlayOneShot(deathAudio);
        }
    }

    // Damage to player.
    private float nextActionTime = 0.0f;
    public float period = 2f;

    void Update()
    {
        float distance = Vector3.Distance(Player.position, transform.position);
        if (distance <= AttackRadious)
        {
            if (Time.time > nextActionTime)
            {
                nextActionTime = Time.time + period;
                healthPlayer.health -= Damage;
            }
        }
    }
    void LateUpdate()
    {
        if (health <= HealthToShowDamage)
        {
            EyeSurfes.GetComponent<MeshRenderer>().material = MaterialEyesurfesDamaged;
        }
    }


    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, AttackRadious);

    }

    // Death to the Enemy
    void Die()
    {
        Destroy(gameObject);
        scoreScript.score += ScoreAdded;
        Instantiate(effect, effecttransform.position, effecttransform.rotation);
        healthPlayer.health += GiveHealth;
    }
}
