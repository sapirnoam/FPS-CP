using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioCash : MonoBehaviour
{
    public AudioClip oneToken;
    public AudioClip  TenTokens;
    public AudioClip  FiftyTokens;
    public AudioClip  HundredTokens;
    public AudioSource Source;

    // Start is called before the first frame update

    // Update is called once per frame
    public void oneTokenCash()
    {
        Source.PlayOneShot( oneToken, 0.7F);
    }
    public void TenTokenCash(string ItemName)
    {
        if (ItemName == "10 Tokens")
        {
            Source.PlayOneShot(TenTokens, 0.7F);
        }
    }

    public void FiftyTokenCash(string ItemName)
    {
        Source.PlayOneShot( FiftyTokens, 0.7F);
    }

    public void HundredTokenCash(string ItemName)
    {
        Source.PlayOneShot( HundredTokens, 0.7F);
    }
}
