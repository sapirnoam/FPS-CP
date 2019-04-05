using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioZone : MonoBehaviour
{
    public Collider theCollider;
    public AudioSource audioSource;

    public void Start()
    {
        theCollider = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            audioSource.Play();
            audioSource.loop = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            audioSource.Stop();
            audioSource.loop = false;

        }
    }
}
