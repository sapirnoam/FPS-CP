using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HelperPanel : MonoBehaviour
{
    public GameObject helper;
    public GameManager gm;

    private bool toggle;
    // Start is called before the first frame update
    void Start()
    {
        helper.SetActive(false);        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            toggle = !toggle;

            if (toggle)
            {
                helper.SetActive(true);
                gm.CursorLock = false;
            }
            if (!toggle)
            {
                gm.CursorLock = true;
                helper.SetActive(false);
            }
        }
    }
}
