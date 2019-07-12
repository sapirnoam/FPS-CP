using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.EventSystems;

namespace MalbersAnimations.Selector
{
    [CustomEditor(typeof(SelectorEditor))]
    public class SelectorEditorEditor : Editor
    {
        SelectorEditor M;
        MonoScript script;

        private void OnEnable()
        {
            M = (SelectorEditor)target;
            script = MonoScript.FromMonoBehaviour((SelectorEditor)target);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.BeginVertical(MalbersEditor.StyleBlue);
            EditorGUILayout.HelpBox("Manage the distribution of all Items\nItems are always child of this gameObject", MessageType.None);
            EditorGUILayout.EndVertical();

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.BeginVertical(MalbersEditor.StyleGray);

            EditorGUI.BeginDisabledGroup(true);
            script = (MonoScript)EditorGUILayout.ObjectField("Script", script, typeof(MonoScript), false);
            EditorGUI.EndDisabledGroup();


            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            EditorGUI.BeginChangeCheck();
            M.SelectorCamera = (Camera)EditorGUILayout.ObjectField(new GUIContent("Selector Camera", "Camera for the selector"), M.SelectorCamera, typeof(Camera), true);
            if (EditorGUI.EndChangeCheck())
                AddRaycaster();


            if (M.SelectorCamera)
            {
                M.WorldCamera = EditorGUILayout.Toggle(new GUIContent("World Spacing", "The camera is no longer child of the Selector\nIt should be child of the Main Camera, Use this when the the Selector on the hands of a character, on VR mode, etc"), M.WorldCamera);

                if (!M.WorldCamera)
                {

                    EditorGUI.BeginChangeCheck();
                    M.CameraOffset = EditorGUILayout.FloatField(new GUIContent("Offset", "Camera Forward Offset"), M.CameraOffset);
                    M.CameraPosition = EditorGUILayout.Vector3Field(new GUIContent("Position", "Camera Position Offset"), M.CameraPosition);
                    M.CameraRotation = EditorGUILayout.Vector3Field(new GUIContent("Rotation", "Camera Rotation Offset"), M.CameraRotation);
                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(M, "Change Camera Values");
                        M.SetCamera();
                    }
                }
            }

            if (M.transform.childCount != M.Items.Count)
            {
                M.UpdateItemsList();
                EditorUtility.SetDirty(target);
            }

            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            EditorGUI.BeginChangeCheck();
            M.SelectorType = (SelectorType)EditorGUILayout.EnumPopup(new GUIContent("Selector Type", "Items Distribution"), M.SelectorType);

            EditorGUI.BeginChangeCheck();
            M.ItemRendererType = (ItemRenderer)EditorGUILayout.EnumPopup(new GUIContent("Items Type", "Items Renderer Type"), M.ItemRendererType);
            if (EditorGUI.EndChangeCheck())
                AddRaycaster();

            EditorGUILayout.EndVertical();
            GUIContent DistanceName = new GUIContent("Radius", "Radius of the selector");
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            switch (M.SelectorType)
            {
                case SelectorType.Radial:
                    M.distance = EditorGUILayout.FloatField(DistanceName, M.distance);
                    M.RadialAxis = (RadialAxis)EditorGUILayout.EnumPopup(new GUIContent("Axis", "Radial Axis"), M.RadialAxis);
                    EditorGUILayout.BeginHorizontal();
                    M.UseWorld = GUILayout.Toggle(M.UseWorld, new GUIContent("Use World Rotation", "The Items will keep the same initial Rotation"), EditorStyles.miniButton);
                    M.LookRotation = GUILayout.Toggle(M.LookRotation, new GUIContent("Use Look Rotation", "The items will look to the center of the selector"),  EditorStyles.miniButton);
                   
                    EditorGUILayout.EndHorizontal();
                    break;
                case SelectorType.Linear:
                    DistanceName = new GUIContent("Distance", "Distance between objects");
                    M.LookRotation = false;
                    EditorUtility.SetDirty(target);
                    serializedObject.ApplyModifiedProperties();
                    M.distance = EditorGUILayout.FloatField(DistanceName, M.distance);
                    M.LinearX = EditorGUILayout.Slider(new GUIContent("Linear X"), M.LinearX, -1, 1);
                    M.LinearY = EditorGUILayout.Slider(new GUIContent("Linear Y"), M.LinearY, -1, 1);
                    M.LinearZ = EditorGUILayout.Slider(new GUIContent("Linear Z"), M.LinearZ, -1, 1);
                    M.UseWorld = false;
                    break;
                case SelectorType.Grid:
                    M.UseWorld = false;
                    EditorGUILayout.BeginHorizontal();
                    EditorGUIUtility.labelWidth = 65;
                    M.Grid = EditorGUILayout.IntField(new GUIContent("Columns", "Ammount of the Columns for the Grid. the Rows are given my the ammount of Items"), M.Grid, GUILayout.MinWidth(100));
                    EditorGUIUtility.labelWidth = 15;
                    M.GridWidth = EditorGUILayout.FloatField(new GUIContent("W", "Width"), M.GridWidth, GUILayout.MinWidth(50));
                    M.GridHeight = EditorGUILayout.FloatField(new GUIContent("H", "Height"), M.GridHeight, GUILayout.MinWidth(50));
                    EditorGUIUtility.labelWidth = 0;
                    EditorGUILayout.EndHorizontal();
                    break;
                case SelectorType.Custom:

                    if (GUILayout.Button(new GUIContent("Store Item Location", "Store the Initial Pos/Rot/Scale of every Item")))
                    {
                        M.StoreCustomLocation();
                    }
                    break;
                default:
                    break;
            }

            M.RotationOffSet = EditorGUILayout.Vector3Field(new GUIContent("Rotation Offset", "Offset for the Rotation on the Radial Selector"), M.RotationOffSet);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(M, "Editor Selector Changed");
                M.LinearVector = new Vector3(M.LinearX, M.LinearY, M.LinearZ);  //Set the new linear vector

                if (M.SelectorType == SelectorType.Custom)
                {
                    if (EditorUtility.DisplayDialog("Use Current Distribution", "Do you want to save the current distribution as a Custom Type Selector", "Yes", "No"))
                    {
                        M.StoreCustomLocation();
                    }
                }

                M.ItemsLocation();
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUI.indentLevel++;
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("Items"), new GUIContent("Items"),true);
            EditorGUI.EndDisabledGroup();
            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();
            GUILayout.BeginHorizontal();

            GUILayout.EndHorizontal();


            //if (M.SelectorCamera == null)
            //{
            //    EditorGUILayout.HelpBox("No camera found, please add a Menu Camera", MessageType.Warning);
            //}


            EditorGUILayout.EndVertical();


            if (EditorGUI.EndChangeCheck())
            {
                Undo.RegisterCompleteObjectUndo(M, "Editor Values Changed");
                EditorUtility.SetDirty(target);
                serializedObject.ApplyModifiedProperties();
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void AddRaycaster()
        {
            if (M.SelectorCamera)
            {
                DestroyImmediate(M.SelectorCamera.GetComponent<BaseRaycaster>());
                switch (M.ItemRendererType)
                {
                    case ItemRenderer.Mesh:
                        PhysicsRaycaster Ph = M.SelectorCamera.gameObject.AddComponent<PhysicsRaycaster>();
                        Ph.eventMask = 32;
                        break;
                    case ItemRenderer.Sprite:
                        Physics2DRaycaster Ph2d = M.SelectorCamera.gameObject.AddComponent<Physics2DRaycaster>();
                        Ph2d.eventMask = 32;
                        break;
                    case ItemRenderer.Canvas:
                        break;
                    default:
                        break;
                }
            }
        }
    }
}