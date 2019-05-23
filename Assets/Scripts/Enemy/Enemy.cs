using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 50f; // Enemy health.
    public float GiveHealth = 6;
    public float ScoreAdded = 10; // The money value that should be added after enemy death.
    public Score scoreScript; // Get the Money information.

    // Managers //
    public CrossHairManager crossHairManager;


    //Materials / Damage//
    //Damage
    public Material EyeDamage; //Eye
    public Material EyeBaseDamage; //Eyebase
    public Material BodyDamage; //Body

    //Hard Damage
    public Material EyeHardDamage; //Eye
    public Material EyeBaseHardDamage; //Eyebase
    public Material BodyHardDamage; //Body

    //Body parts
    public GameObject Body; //object
    public GameObject EyeSurfes; //object
    public GameObject Eye1;
    public GameObject Eye2;

    //Floats to change materials
    public float HealthToShowDamage = 30; //floats
    public float HealthToShowHardDamage = 15; //floats

    //GORE//
    public Transform effecttransform; // The transform for the effect
    public AudioClip[] AudioImpact; //Impact audio
    public GameObject effect; // The death effect


    // Attacking the player components ; Attacking while player tuches the enemy!, Not weapon attacking.//


    public float Damage = 20; // Damage x to the player
    Transform Player; // Get Player Transform for the gizmo
    public Health healthPlayer; // Player Health script
    public float AttackRadious = 10; // How far the enemy hit
    public RainCameraController BloodController; // On Screen blood effect for damage

    // Audio //
    public AudioSource AudioS;
    public AudioClip[] Death;
    public AudioClip[] Gore;
    public AudioClip[] DamageS;
    private bool HealthSound1 = false;
    private bool HealthSound2 = false;

    // Getting components.
    void Start()
    {
        effecttransform = this.gameObject.transform.GetChild(4);
        AudioS = GameObject.Find("Ambient Audio").GetComponent<AudioSource>();
        scoreScript = FindObjectOfType<Score>();
        crossHairManager = FindObjectOfType<CrossHairManager>();
        healthPlayer = FindObjectOfType<Health>();
        BloodController = GameObject.Find("Splatter Camera").GetComponent<RainCameraController>();
        Player = PlayerManager.instance.player.transform;
    }

    // Damage to the Enemy.
    public void TakeDamage(float amount)
    {
        crossHairManager.Hit = true;
        health -= amount;
        AudioS.PlayOneShot(AudioImpact[Random.Range(0, AudioImpact.Length)]);
        if (health <= 0f)
        {
            AudioS.PlayOneShot(Death[Random.Range(0, Death.Length)]);
            Die();
        }
    }

    // Damage to player.
    private float nextActionTime = 0.0f;
    public float Attackperiod = 2f;

    void Update()
    {
        float distance = Vector3.Distance(Player.position, transform.position);
        if (distance <= AttackRadious)
        {
            if (Time.unscaledTime > nextActionTime)
            {
                nextActionTime = Time.unscaledTime + Attackperiod;
                healthPlayer.health -= Damage;
                if (healthPlayer.health <= 80)
                {
                    BloodController.Play();
                }
            }
        }
        if (health <= HealthToShowDamage)
        {
            Eye1.GetComponent<MeshRenderer>().material = EyeDamage;
            Eye2.GetComponent<MeshRenderer>().material = EyeDamage;
            Body.GetComponent<MeshRenderer>().material = BodyDamage;
            EyeSurfes.GetComponent<MeshRenderer>().material = EyeBaseDamage;
            HealthToShowDamageS();
        }
        if (health <= HealthToShowHardDamage)
        {
            Eye1.GetComponent<MeshRenderer>().material = EyeHardDamage;
            Eye2.GetComponent<MeshRenderer>().material = EyeHardDamage;
            Body.GetComponent<MeshRenderer>().material = BodyHardDamage;
            EyeSurfes.GetComponent<MeshRenderer>().material = EyeBaseHardDamage;
            HealthToShowDamageHardS();
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
        AudioS.PlayOneShot(Gore[Random.Range(0, Gore.Length)]);
    }
    public void DieWithOutGive()
    {
        Destroy(gameObject);
        Instantiate(effect, effecttransform.position, effecttransform.rotation);
        AudioS.PlayOneShot(Gore[Random.Range(0, Gore.Length)]);
        Debug.Log("Exectute");
    }

    void HealthToShowDamageS()
    {
        if (HealthSound1 == false)
        {
            /* AudioS.PlayOneShot(DamageS[Random.Range(0, DamageS.Length)]); */
            HealthSound1 = true;
        }

    }
    void HealthToShowDamageHardS()
    {
        if(HealthSound2 == false)
        {
            /* AudioS.PlayOneShot(DamageS[Random.Range(0, DamageS.Length)]); */
            HealthSound2 = true;
        }
    }
}
