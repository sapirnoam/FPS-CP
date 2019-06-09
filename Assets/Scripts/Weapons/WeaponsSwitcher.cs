using UnityEngine;

public class WeaponsSwitcher : MonoBehaviour
{

    public int selectedWeapon = 0;
    public bool IsReloading = false;
    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsReloading == false)
        {
            int previousSelectedWeapon = selectedWeapon;

            if (Input.GetAxis("Mouse ScrollWheel") > 0f || Input.GetButtonDown("Left Bump") || Input.GetKeyDown("=")) // Should assign a buttons to this void (Input settings) for controller support
            {
                if (Time.timeScale >= 0.5)
                {
                    if (selectedWeapon >= transform.childCount - 1 && Time.timeScale >= 0.5)
                        selectedWeapon = 0;
                    else
                        selectedWeapon++;
                }
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0f || Input.GetButtonDown("Right Bump") || Input.GetKeyDown("-")) // Should assign a buttons to this void (Input settings) for controller support
            {
                if (Time.timeScale >= 0.5)
                {
                    if (selectedWeapon <= 0)
                        selectedWeapon = transform.childCount - 1;
                    else
                        selectedWeapon--;
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                selectedWeapon = 0;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2 && Time.timeScale >= 0.5)
            {
                selectedWeapon = 1;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3 && Time.timeScale >= 0.5)
            {
                selectedWeapon = 2;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4 && Time.timeScale >= 0.5)
            {
                selectedWeapon = 3;
            }
            if (Input.GetKeyDown(KeyCode.Alpha5) && transform.childCount >= 5 && Time.timeScale >= 0.5)
            {
                selectedWeapon = 4;
            }
            if (Input.GetKeyDown(KeyCode.Alpha6) && transform.childCount >= 6 && Time.timeScale >= 0.5)
            {
                selectedWeapon = 5;
            }
            if (Input.GetKeyDown(KeyCode.Alpha7) && transform.childCount >= 7 && Time.timeScale >= 0.5)
            {
                selectedWeapon = 6;
            }
            if (Input.GetKeyDown(KeyCode.Alpha8) && transform.childCount >= 8 && Time.timeScale >= 0.5)
            {
                selectedWeapon = 7;
            }
            if (Input.GetKeyDown(KeyCode.Alpha9) && transform.childCount >= 9 && Time.timeScale >= 0.5)
            {
                selectedWeapon = 8;
            }
            if (Input.GetKeyDown(KeyCode.Alpha0) && transform.childCount >= 0 && Time.timeScale >= 0.5)
            {
                selectedWeapon = 9;
            }
            if (previousSelectedWeapon != selectedWeapon)
            {
                SelectWeapon();
            }
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
}
