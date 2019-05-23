using System.Collections;
using UnityEngine;
using System.Collections.Generic;
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
    IEnumerator Die()
    {
        IsDead = true;
        mainCamera.SetActive(false);
        DeathCamera.SetActive(true);
        Instantiate(effectBlood, effecttransform.position, effecttransform.rotation);
        Instantiate(effect, effecttransform.position, effecttransform.rotation);
        yield return new WaitForSeconds(0.2f);

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject target in enemies)
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance < 50)
            {
                target.GetComponent<Enemy>().DieWithOutGive();
            }
        }
        Destroy(gameObject);
        yield return new WaitForSeconds(1f);
        //End game scene. Please forward to other script in this line.
        //Because of the player destroy this script also destroind. PS. Once you forwarding move the Destroy(gaameObject) down.!
    }
}
