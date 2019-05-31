using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDestroy : MonoBehaviour
{
    public float DestroyWaitTime = 20;

    private void Start()
    {
        StartCoroutine(Destroy());   
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(DestroyWaitTime);
        Destroy(gameObject);
    }
}
