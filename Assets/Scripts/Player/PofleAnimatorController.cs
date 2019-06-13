using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;

public class PofleAnimatorController : MonoBehaviour
{
    public Animator anim;
    bool m_IsWalking;
    bool IsDead = false;


    public AudioSource AudioS;
    public Transform effecttransform; // The transform for the effect
    public GameObject effect; // The death effect
    public GameObject effectBlood; // The death effect
    public Health healthManager;
    public Score score;
    public void Start()
    {
        effecttransform = this.gameObject.transform.GetChild(4);
        effecttransform.GetComponent<Transform>();
        healthManager = FindObjectOfType<Health>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            anim.SetBool("IsWalking", true);
        }
        if (!Input.GetButton("Horizontal") && !Input.GetButton("Vertical"))
        {
            anim.SetBool("IsWalking", false);
        }
        if (IsDead == false && healthManager.health < 1)
        {
            StartCoroutine(Die());
        }
    }
    public GameObject MainCamera;


    public GameObject DeathCamera;
    public GameObject mainCamera;
    GameObject[] Weapons;
    public IEnumerator Die()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Weapons = GameObject.FindGameObjectsWithTag("Weapon");
        for (var i = 0; i < Weapons.Length; i++)
            Destroy(Weapons[i]);
        IsDead = true;
        mainCamera.SetActive(false);
        DeathCamera.SetActive(true);
        Instantiate(effect, effecttransform.position, effecttransform.rotation);
        yield return new WaitForSeconds(0.2f);
        foreach (GameObject target in enemies)
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance < 55)
            {
                target.GetComponent<Enemy>().DieWithOutGive();
            }
        }
        score.PofleAnimator();
        Destroy(gameObject);
    }
}
