using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Skin : MonoBehaviour
{
    public int Price = 0;
    public bool IsOwned = false;
    public bool IsSelected = false;
    public int ID = 0;
    public GameObject SkinObject;
    public Text TextObject;
    public Button ButtonText;
    public SkinsManager skinsManager;
    public Platinums platinumsManager;

    void Start()
    {
        //Load from gj
        if (IsOwned == true)
        {
            if (IsSelected == true)
            {
                TextObject.text = "SELECTED".ToString();
                //ButtonText.colors
                skinsManager.IDSelected = ID;
            }
            else
            {
                TextObject.text = "Select".ToString();
                //ButtonText.colors

            }
        }
        else
        {
            TextObject.text = (Price + "P".ToString());
            //ButtonText.colors

        }
    }
    public void UnSelect()
    {
        TextObject.text = "Select".ToString();
        //ButtonText.colors
        IsSelected = false;
        //save to gj
    }
    private Skin skin;
    public void Equip()
    {
        if (IsOwned == true)
        {
            //skin = skinsManager.SelectedSkinScript;
            skin.IsSelected = false;
            skin.TextObject.text = "Select".ToString();

            //GO TO ALL THE SKIN SCRIPTS AND EXTRUDE UNQUIP
            IsSelected = true;

            TextObject.text = "SELECTED".ToString();
            skinsManager.IDSelected = ID;
            //skinsManager.SelectedSkinScript = this;
            //ButtonText.colors
            //save to gj
        }

    }
    public void Purchase()
    {
        if (platinumsManager.platinums >= Price)
        {
            platinumsManager.platinums -= Price;
            IsOwned = true;
            IsSelected = true;
            TextObject.text = "SELECTED".ToString();
            skinsManager.IDSelected = ID;

            //ButtonText.colors

            //Instantiate ID Skin and apply to the manager.
            //save to gj the purchase + Save the platinums level.
        }
    }
}
