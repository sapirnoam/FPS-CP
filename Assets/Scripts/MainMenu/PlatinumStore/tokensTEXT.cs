using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tokensTEXT : MonoBehaviour
{
    public Tokens token;
    public Text TOKENtext;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TOKENtext.text = ((int)token.TOKENS).ToString();
    }
}
