using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameJolt.API;
public class BulletPlay : MonoBehaviour
{
    public Animator anim;
    public AudioClip ShotAudioClip;
    public AudioSource audioS;
    public GameObject playPanel;
    public void PoflesSlayer()
    {
        if (GameJoltAPI.Instance.HasUser)
        {
            StartCoroutine(PoflesSlayerPlay());
        }
    }
    public void Story()
    {
        if (GameJoltAPI.Instance.HasUser)
        {
            Debug.Log("Not available");
        }
    }
    IEnumerator PoflesSlayerPlay()
    {
        anim.SetTrigger("PlaySlayer");
        playPanel.SetActive(true);
        audioS.PlayOneShot(ShotAudioClip);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }
}
