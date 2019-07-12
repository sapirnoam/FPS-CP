using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
    {
        public bool CursorLock;
        private bool IsLocked;
        public bool IsDead = false;
        public int FpsTarget = 150;
        // Start is called before the first frame update
        void Start()
        {
            CursorLock = true;
        Application.targetFrameRate = FpsTarget;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            if (CursorLock == true) // visible
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            if (CursorLock == false) // locked
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("NewsShowed", 0);
        PlayerPrefs.SetInt("BackedFromGame", 0);
    }
}
