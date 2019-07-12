using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SkinManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PofleVarod;
    public GameObject PofleYarok;
    public GameObject ZombiePofle;
    public GameObject PofleSagol;
    public string ID;
    void Start()
    {
        ID = PlayerPrefs.GetString("CharacterID");
        var CharacterID = new GameObject(ID);
        Scene Scene = SceneManager.GetActiveScene();
        if (Scene.name == "Pofle'sSlayer");
        {
            Instantiate(CharacterID, transform.position, transform.rotation);
        }
    }
    public void PofleVarodSelect()
    {
        ID = "PofleVarod";
        PlayerPrefs.SetString("CharacterID", ID);
        PlayerPrefs.Save();
    }
    public void PofleSagolSelect()
    {
        ID = "PofleSagol";
        PlayerPrefs.SetString("CharacterID", ID);
        PlayerPrefs.Save();
    }
    public void ZombiePofleSelect()
    {
        ID = "ZombiePofle";
        PlayerPrefs.SetString("CharacterID", ID);
        PlayerPrefs.Save();
    }
}
