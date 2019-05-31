using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BUYTOKENS : MonoBehaviour
{
    public Tokens tokens;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Purchase10()
    {
        tokens.TOKENS += 10;
        Debug.Log("Success 10+");
    }
    public void Purchase50()
    {
        tokens.TOKENS += 50;
    }
    public void Purchase100()
    {
        tokens.TOKENS += 100;
    }
}
