using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Registration : MonoBehaviour
{
    public InputField nameField;
    public InputField passwordFiled;
    public InputField EmailFiled;

    public Button sumbitButton;

    public void CallRegister()
    {
        StartCoroutine(Register());
    }

    IEnumerator Register()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", nameField.text);
        form.AddField("password", passwordFiled.text);

        WWW www = new WWW("https://poflesql.000webhostapp.com/sqlconnect/register.php", form);
        yield return www;
        if (www.text == "0")
        {
            Debug.Log("User created");
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        else
        {
            Debug.Log("User creation failed. Error #" + www.text);
        }
    }

    public void VeriftInputs()
    {
        sumbitButton.interactable = (nameField.text.Length >= 4 && passwordFiled.text.Length >= 6);
    }

}
