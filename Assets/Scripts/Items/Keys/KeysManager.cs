using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeysManager : MonoBehaviour
{
    public GameObject Key;
    public Text keys;
    public GameObject KeysImage;
    public GameObject KeysText;

    public int Keys = 0;


    public void LateUpdate()
    {
        keys.text = Keys.ToString();
    }
}
