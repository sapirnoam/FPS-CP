using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalChest : MonoBehaviour
{
    public GameObject destroyedVersion;

    public void OnOpenCrystal()
    {
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
