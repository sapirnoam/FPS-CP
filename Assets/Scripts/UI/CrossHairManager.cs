using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairManager : MonoBehaviour
{
    private int CrossHairNumber = 0;

    public bool Hit = false;

    public GameObject HitObject;
    private void Start()
    {
        Hit = false;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Hit == true)
        {
            StartCoroutine(HitEnumerator());
        }
        if(Hit == false)
        {
            HitObject.SetActive(false);        
        }
    }

    IEnumerator HitEnumerator()
    {
        HitObject.SetActive(true);
        yield return new WaitForSeconds(0.050f);
        Hit = false;

    }
}
