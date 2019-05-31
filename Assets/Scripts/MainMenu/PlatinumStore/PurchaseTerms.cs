using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseTerms : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Open()
    {
        Application.OpenURL("http://www.noam3d.com/terms-of-purchase");
    }
}
