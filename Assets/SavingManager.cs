using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SavingManager : MonoBehaviour
{
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Button button5;



    private void OnDisable()
    {
        button1.interactable = true;
        button2.interactable = true;
        button3.interactable = true;
        button4.interactable = true;
        button5.interactable = true;
    }
    private void OnEnable()
    {
        button1.interactable = false;
        button2.interactable = false;
        button3.interactable = false;
        button4.interactable = false;
        button5.interactable = false;
    }
}
