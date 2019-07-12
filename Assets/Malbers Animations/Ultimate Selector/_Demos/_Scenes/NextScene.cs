using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MalbersAnimations;
using UnityEngine.UI;
public class NextScene : MonoBehaviour {

    public InputRow Input = new InputRow(KeyCode.N);
    public bool LockCursor;
    void Start()
    {
        Cursor.lockState = LockCursor ? CursorLockMode.Locked : CursorLockMode.None;  // Lock or unlock the cursor.
        Cursor.visible = !LockCursor;
    }
	
	void Update () {

        if (Input.GetInput)
        {
            GetComponent<Button>().onClick.Invoke();
        }
	}
}
