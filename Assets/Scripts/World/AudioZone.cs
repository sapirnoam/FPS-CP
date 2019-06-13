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
            StartCoroutine(Enter());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(Exit());

        }
    }
    IEnumerator Exit()
    {
        audioSource.volume = 0.8f;
        yield return new WaitForSeconds(0.1f);
        audioSource.volume = 0.5f;
        yield return new WaitForSeconds(0.1f);
        audioSource.volume = 0.3f;
        yield return new WaitForSeconds(0.1f);
        audioSource.volume = 0.1f;
        yield return new WaitForSeconds(0.1f);
        audioSource.volume = 0f;
    }
    IEnumerator Enter()
    {
        audioSource.volume = 0.2f;
        yield return new WaitForSeconds(0.1f);
        audioSource.volume = 0.5f;
        yield return new WaitForSeconds(0.1f);
        audioSource.volume = 0.7f;
        yield return new WaitForSeconds(0.1f);
        audioSource.volume = 0.8f;
        yield return new WaitForSeconds(0.1f);
        audioSource.volume = 1f;
        audioSource.loop = true;
    }
}
