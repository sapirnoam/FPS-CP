using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMap : MonoBehaviour
{
    public GameObject map;
    public GameManager gm;
    private bool toggle;

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown("m"))
        {
            toggle = !toggle;
            if (toggle)
            {
                map.SetActive(true);
                gm.CursorLock = false;
            }
            if (!toggle)
            {
                gm.CursorLock = true;
                map.SetActive(false);
            }
        }
    }
}
