using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using KinematicCharacterController;
using KinematicCharacterController.Examples;

namespace KinematicCharacterController.Walkthrough.NavMeshExample
{
    public class MyAI : MonoBehaviour
    {
        public MyCharacterController Character;
        public Transform Destination;

        private NavMeshPath _path;
        private Vector3[] _pathCorners = new Vector3[16];
        private Vector3 _lastValidDestination;

        private void Start()
        {
            _path = new NavMeshPath();
        }

        private void Update()
        {
            HandleCharacterNavigation();
        }
        
        private void HandleCharacterNavigation()
        {
            if(NavMesh.CalculatePath(Character.transform.position, Destination.position, NavMesh.AllAreas, _path))
            {
                _lastValidDestination = Destination.position;
            }
            else
            {
                NavMesh.CalculatePath(Character.transform.position, _lastValidDestination, NavMesh.AllAreas, _path);
            }

            AICharacterInputs characterInputs = new AICharacterInputs();

            int cornersCount = _path.GetCornersNonAlloc(_pathCorners);
            if (cornersCount > 1)
            {
                // Build the CharacterInputs struct
                characterInputs.MoveVector = (_pathCorners[1] - Character.transform.position).normalized;

                // Apply inputs to character
                Character.SetInputs(ref characterInputs);
            }
            else
            {
                // Build the CharacterInputs struct
                characterInputs.MoveVector = Vector3.zero;

                // Apply inputs to character
                Character.SetInputs(ref characterInputs);
            }
        }
    }
}