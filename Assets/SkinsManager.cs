using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinsManager : MonoBehaviour
{
    public int IDSelected = 0;
    public int PlayerLevel = 0;
    public List<GameObject> Lockers;
    public RankManager rankManager;
    public Skin SelectedSkinScript;
    private void Start()
    {
        InvokeRepeating("CheckForLevelUp", 2f, 2f);
    }
    void CheckForLevelUp()
    {
        if(rankManager.Rank >= PlayerLevel)
        {
            //Lockers.Remove(());
            //Save to gj
        }
    }
}
