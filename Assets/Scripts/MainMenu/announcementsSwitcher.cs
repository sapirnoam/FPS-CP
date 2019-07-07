using UnityEngine;
using UnityEngine.UI;
public class announcementsSwitcher : MonoBehaviour
{
    public int selectedWeapon = 0;
    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    public void LeftButton()
    {
        int previousSelectedWeapon = selectedWeapon;
        if (selectedWeapon >= transform.childCount - 1 && Time.timeScale >= 0.5)
            selectedWeapon = 0;
        else
            selectedWeapon++;
        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }
    public void RightButton()
    {
        int previousSelectedWeapon = selectedWeapon;
        if (selectedWeapon <= 0)
            selectedWeapon = transform.childCount - 1;
        else
            selectedWeapon--;
        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }
    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
    private bool toggle;
    public GameObject Announcments;
    public Text HideText;
    public void ShowHide()
    {
        toggle = !toggle;
        if (toggle)
        {
            Announcments.SetActive(true);
            HideText.text = "HIDE".ToString();
        }
        if (!toggle)
        {
            Announcments.SetActive(false);
            HideText.text = "SHOW".ToString();
        }
    }
    public void Patreon()
    {
        Application.OpenURL("https://www.patreon.com/Noam3D/");
    }
    public void CheckPatchNews()
    {
        Application.OpenURL("https://noam3d.com/closed-testing-v5-huge-update-gamejolt/");
    }
}