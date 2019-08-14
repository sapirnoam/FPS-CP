using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunDroper : MonoBehaviour
{
    public GameObject Weapon;
    public GameObject EffectDrop;
    private GameObject temp;
    public GameObject[] DropPoints;
    private int SelectedDropIndex;
    public GameObject DropperLocationToFollow;
    private GameObject Selected;

    private void Start()
    {
        SelectedDropIndex = Random.Range(0, DropPoints.Length);
        DropperLocationToFollow = DropPoints[SelectedDropIndex];

        Instantiate(Weapon, DropperLocationToFollow.transform);
    }

}
