using UnityEngine;
public class PofleAnimatorController : MonoBehaviour
{
    public Animator anim;
    bool m_IsWalking;

    public void Start()
    {
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
    }
}
