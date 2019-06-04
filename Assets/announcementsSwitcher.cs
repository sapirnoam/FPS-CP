using UnityEngine;
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

    public void Patreon()
    {
        Application.OpenURL("https://www.patreon.com/Noam3D/");
    }
}