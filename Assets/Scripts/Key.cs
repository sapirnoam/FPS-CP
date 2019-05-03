using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public KeysManager Km;
    // Start is called before the first frame update
    void Start()
    {
        Km = FindObjectOfType<KeysManager>();
    }
    void OnCollisionEnter(Collision other)
    {
            Km.Keys += 1;
            Destroy(gameObject);
    }
}

