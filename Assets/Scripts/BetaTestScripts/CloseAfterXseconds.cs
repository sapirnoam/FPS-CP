using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAfterXseconds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Close());
    }

    public void Update()
    {
        if(Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    IEnumerator Close()
    {
        yield return new WaitForSeconds(6);
        Application.Quit();
    }
}
