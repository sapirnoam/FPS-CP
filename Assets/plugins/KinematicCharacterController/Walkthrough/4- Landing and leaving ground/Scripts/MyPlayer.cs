using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;
using KinematicCharacterController.Examples;
using UnityEngine.UI;

namespace KinematicCharacterController.Walkthrough.LandingLeavingGround
{
    public class MyPlayer : MonoBehaviour
    {
        public OrbitCamera OrbitCamera;
        public Transform CameraFollowPoint;
        public MyCharacterController Character;
        public float MouseSensitivity = 0.01f;
        public float MaxDistanceAjustable = 5f;
        

        private const string MouseXInput = "Mouse X";
        private const string MouseYInput = "Mouse Y";
        private const string MouseScrollInput = "Mouse ScrollWheel";
        private const string HorizontalInput = "Horizontal";
        private const string VerticalInput = "Vertical";
        private bool toggle;
        public bool isTPS = true;

        private void Start()
        {
            // Tell camera to follow transform
            OrbitCamera.SetFollowTransform(CameraFollowPoint);

            // Ignore the character's collider(s) for camera obstruction checks
            OrbitCamera.IgnoredColliders = Character.GetComponentsInChildren<Collider>();
            if (OrbitCamera.MaxDistance >= 5)
            {
                isTPS = true;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown("v"))
            {
                toggle = !toggle;

                if (toggle)
                {
                    OrbitCamera.MaxDistance = 0f; //FPS
                    isTPS = false;

                }

                else
                {
                    OrbitCamera.MaxDistance = MaxDistanceAjustable; //RTS
                    isTPS = true;
                    transform.rotation = Quaternion.Euler(transform.rotation.x, 0, 0);
                }
            }

            //Change Camera distance:
            if (isTPS == true)
            {
                if (Input.GetKey(KeyCode.PageUp) && isTPS == true && OrbitCamera.MaxDistance <= 22)
                {
                    OrbitCamera.MaxDistance += 1;
                }
                if (Input.GetKey(KeyCode.PageDown) && isTPS == true && OrbitCamera.MaxDistance >= 5)
                {
                    OrbitCamera.MaxDistance -= 1;
                }
            }


            HandleCameraInput();
            HandleCharacterInput();
        }
            private void HandleCameraInput()
        {
            // Create the look input vector for the camera
            float mouseLookAxisUp = Input.GetAxisRaw(MouseYInput);
            float mouseLookAxisRight = Input.GetAxisRaw(MouseXInput);
            Vector3 lookInputVector = new Vector3(mouseLookAxisRight * MouseSensitivity, mouseLookAxisUp * MouseSensitivity, 0f);

            // Prevent moving the camera while the cursor isn't locked
            if (Cursor.lockState != CursorLockMode.Locked)
            {
                lookInputVector = Vector3.zero;
            }

            // Input for zooming the camera (disabled in WebGL because it can cause problems)

            float scrollInput = -Input.GetAxis(MouseScrollInput);
#if UNITY_WEBGL
        scrollInput = 0f;
#endif

            // Apply inputs to the camera
            OrbitCamera.UpdateWithInput(Time.deltaTime, Time.deltaTime /* The float scrollInput from above should be here instand of Time.deltaTime for view ajustment! */, lookInputVector );

            // Handle toggling zoom level - DISABLED!

/*
            if (Input.GetMouseButtonDown(1))
            {
                OrbitCamera.TargetDistance = (OrbitCamera.TargetDistance == 0f) ? OrbitCamera.DefaultDistance : 0f;
            }
*/
        }

        private void HandleCharacterInput()
        {
            PlayerCharacterInputs characterInputs = new PlayerCharacterInputs();

            // Build the CharacterInputs struct
            characterInputs.MoveAxisForward = Input.GetAxisRaw(VerticalInput);
            characterInputs.MoveAxisRight = Input.GetAxisRaw(HorizontalInput);
            characterInputs.CameraRotation = OrbitCamera.Transform.rotation;
            characterInputs.JumpDown = Input.GetKeyDown(KeyCode.Space);

            // Apply inputs to character
            Character.SetInputs(ref characterInputs);
        }
       
    }
}