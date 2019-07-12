using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CrystalManager : MonoBehaviour
{
    public GameObject CrystalToSummon;
    public GameObject CrystalDestroyedVersion;
    public int CrystalBoxAvailable = 0;
    public bool IsOpening = false;

    public GameObject CrystalInScene;

    public Text ChestAvailable;
    public Text ChestAvailable2;
    public GameObject CrystalAvailableObjects;
    public GameObject CrystalAvailableObjects2;
    public AudioClip[] CrystalSoundBreak;
    public AudioSource AudioS;

    public int Price = 10;
    public void LateUpdate()
    {
        if (CrystalBoxAvailable >= 0)
        {
            ChestAvailable.text = CrystalBoxAvailable.ToString();
            ChestAvailable2.text = CrystalBoxAvailable.ToString();
        }
    }

    public void OpenCrystal()
    {
        if (CrystalBoxAvailable > 0 && IsOpening == false && Enumator == false)
        {
            IsOpening = true;
            StartCoroutine(OpenChestCoroutine()); //Start the Corountine that will be open the crystal step by step.
        }
    }
    public void PurchaseCrystal(Platinums PlatinumManager)
    {
        if (IsOpening == false && Enumator == false && PlatinumManager.platinums >= Price)
        {
            PlatinumManager.platinums -= Price;
            CrystalBoxAvailable += 1;
        }
    }
    private bool Enumator = false;
    IEnumerator OpenChestCoroutine()
    {
        Enumator = true;
        CrystalBoxAvailable -= 1;
        CrystalInScene = GameObject.FindGameObjectWithTag("Crystal");
        Destroy(CrystalInScene);
        Instantiate(CrystalDestroyedVersion, transform.position, transform.rotation);
        AudioS.PlayOneShot(CrystalSoundBreak[Random.Range(0, CrystalSoundBreak.Length)]);
        yield return new WaitForSeconds(5f);
        CrystalInScene = GameObject.FindGameObjectWithTag("BrokenCrystal");
        Destroy(CrystalInScene);
        Instantiate(CrystalToSummon, transform.position, transform.rotation);
        IsOpening = false;
        Enumator = false;
    }
}
