using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
    {
        public bool CursorLock;
        private bool IsLocked;
    public bool IsDead = false;
        // Start is called before the first frame update
        void Start()
        {
            CursorLock = true;
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
}
