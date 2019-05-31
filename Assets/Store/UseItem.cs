using UnityEngine;
using System.Collections;

public class UseItem : MonoBehaviour
{
    public static void Use(string ItemName)
    {
        Debug.Log("Custom item usage executing for item: " + ItemName);

        if (ItemName == "10 Tokens")
        {
            Tokens token = GameObject.Find("TOKENS").GetComponent<Tokens>();
            token.TOKENS += 10;
        }

        if (ItemName == "50 Tokens")
        {
            Tokens token = GameObject.Find("TOKENS").GetComponent<Tokens>();
            token.TOKENS += 50;
        }

        if (ItemName == "100 Tokens")
        {
            Tokens token = GameObject.Find("TOKENS").GetComponent<Tokens>();
            token.TOKENS += 100;
        }
    }
}
