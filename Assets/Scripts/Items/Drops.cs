using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drops : MonoBehaviour
{

    public GameObject[] DropPoints;
    public int SelectedIndex;
    private GameObject Selected;

    public GameObject FirstAid;
    private GameObject temp;
    // Start is called before the first frame update

    // Update is called once per frame
    private void Start()
    {
        InvokeRepeating("Drop", 120f, 120f);
    }
    void Drop()
    {
        SelectedIndex = Random.Range(1, DropPoints.Length);
        temp = Instantiate(FirstAid, DropPoints[SelectedIndex].transform);
        temp.transform.parent = null;
    }
}
