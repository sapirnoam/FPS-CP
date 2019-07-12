using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEditor;
using UnityEditor.Events;
using MalbersAnimations.Events;

namespace MalbersAnimations.Selector
{
    [CustomEditor(typeof(SelectorData))]
    public class SelectorDataEditor : Editor
    {
        SelectorData M;
        MonoScript script;


        private void OnEnable()
        {
            M = (SelectorData)target;
            script = MonoScript.FromScriptableObject((SelectorData)target);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.BeginVertical(MalbersEditor.StyleBlue);
            EditorGUILayout.HelpBox("Data values to save on each Selector", MessageType.None);
            EditorGUILayout.EndVertical();

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.BeginVertical(MalbersEditor.StyleGray);
            {
                EditorGUI.BeginDisabledGroup(true);
                script = (MonoScript)EditorGUILayout.ObjectField("Script", script, typeof(MonoScript), false);
                EditorGUI.EndDisabledGroup();

                EditorGUILayout.BeginVertical(EditorStyles.helpBox);

                EditorGUILayout.PropertyField(serializedObject.FindProperty("usePlayerPref"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("PlayerPrefKey"));
                EditorGUILayout.EndVertical();

                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                SerializedProperty Save = serializedObject.FindProperty("Save");

                GUIStyle newGuiStyle = new GUIStyle(EditorStyles.label)
                {
                    alignment = TextAnchor.MiddleCenter,
                    fontStyle = FontStyle.Bold
                };

                //EditorGUILayout.LabelField("Data to Save", newGuiStyle);
                //EditorGUILayout.BeginVertical(EditorStyles.textField); EditorGUILayout.EndVertical();
                //EditorGUILayout.Separator();

                EditorGUILayout.PropertyField(Save.FindPropertyRelative("FocusedItem"));
                EditorGUILayout.BeginHorizontal();

                M.Save.UseMaterialChanger = GUILayout.Toggle(M.Save.UseMaterialChanger, new GUIContent("Material Changer","The items on the selector have the Material Changer component"), EditorStyles.miniButton);
                M.Save.UseActiveMesh = GUILayout.Toggle(M.Save.UseActiveMesh, new GUIContent("Active Mesh", "The items on the selector have the Active Mesh component"), EditorStyles.miniButton);

                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();


                //-----------------

                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                {
                    EditorGUILayout.LabelField("Coins", newGuiStyle);
                    EditorGUILayout.BeginVertical(EditorStyles.textField); EditorGUILayout.EndVertical();
                    EditorGUILayout.BeginHorizontal();
                    EditorGUI.indentLevel++;
                    EditorGUIUtility.labelWidth = 75;
                    EditorGUILayout.PropertyField(Save.FindPropertyRelative("Coins"), new GUIContent("Current"), true, GUILayout.MinWidth(30));
                    EditorGUILayout.PropertyField(Save.FindPropertyRelative("RestoreCoins"), new GUIContent("Restore"), true, GUILayout.MinWidth(30));
                    EditorGUIUtility.labelWidth = 0;
                    EditorGUI.indentLevel--;
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.EndVertical();

                //-----------------

                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                {
                    EditorGUILayout.LabelField("Locked", newGuiStyle);
                    EditorGUILayout.BeginVertical(EditorStyles.textField); EditorGUILayout.EndVertical();
                    EditorGUILayout.BeginHorizontal();
                    EditorGUI.indentLevel++;
                    EditorGUIUtility.labelWidth = 95;
                    EditorGUILayout.PropertyField(Save.FindPropertyRelative("Locked"), new GUIContent("Current"), true, GUILayout.MinWidth(30));
                    EditorGUILayout.PropertyField(Save.FindPropertyRelative("RestoreLocked"), new GUIContent("Restore"), true, GUILayout.MinWidth(30));
                    EditorGUIUtility.labelWidth = 0;
                    EditorGUI.indentLevel--;
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.EndVertical();


                //---------------------------------------------------------------------------------

                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                {
                    EditorGUILayout.LabelField("Items Amount", newGuiStyle);
                    EditorGUILayout.BeginVertical(EditorStyles.textField); EditorGUILayout.EndVertical();
                    EditorGUILayout.BeginHorizontal();
                    EditorGUI.indentLevel++;
                    EditorGUIUtility.labelWidth = 95;
                    EditorGUILayout.PropertyField(Save.FindPropertyRelative("ItemsAmount"), new GUIContent("Current"), true, GUILayout.MinWidth(30));
                    EditorGUILayout.PropertyField(Save.FindPropertyRelative("RestoreItemsAmount"), new GUIContent("Restore"), true, GUILayout.MinWidth(30));
                    EditorGUIUtility.labelWidth = 0;
                    EditorGUI.indentLevel--;
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.EndVertical();

                //---------------------------------------------------------------------------------

                if (M.Save.UseMaterialChanger)
                {
                    EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                    {
                        EditorGUILayout.LabelField("Material Changer Index", newGuiStyle);
                        EditorGUILayout.BeginVertical(EditorStyles.textField); EditorGUILayout.EndVertical();
                        EditorGUILayout.BeginHorizontal();
                        EditorGUI.indentLevel++;
                        EditorGUIUtility.labelWidth = 95;
                        EditorGUILayout.PropertyField(Save.FindPropertyRelative("MaterialIndex"), new GUIContent("Current"), true, GUILayout.MinWidth(30));
                        EditorGUILayout.PropertyField(Save.FindPropertyRelative("RestoreMaterialIndex"), new GUIContent("Restore"), true, GUILayout.MinWidth(30));
                        EditorGUIUtility.labelWidth = 0;
                        EditorGUI.indentLevel--;
                        EditorGUILayout.EndHorizontal();
                    }
                    EditorGUILayout.EndVertical();
                }

                //---------------------------------------------------------------------------------

                if (M.Save.UseActiveMesh)
                {
                    EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                    {
                        EditorGUILayout.LabelField("Active Meshes Index", newGuiStyle);
                        EditorGUILayout.BeginVertical(EditorStyles.textField); EditorGUILayout.EndVertical();
                        EditorGUILayout.BeginHorizontal();
                        EditorGUI.indentLevel++;
                        EditorGUIUtility.labelWidth = 95;
                        EditorGUILayout.PropertyField(Save.FindPropertyRelative("ActiveMeshIndex"), new GUIContent("Current"), true, GUILayout.MinWidth(30));
                        EditorGUILayout.PropertyField(Save.FindPropertyRelative("RestoreActiveMeshIndex"), new GUIContent("Restore"), true, GUILayout.MinWidth(30));
                        EditorGUIUtility.labelWidth = 0;
                        EditorGUI.indentLevel--;
                        EditorGUILayout.EndHorizontal();
                    }
                    EditorGUILayout.EndVertical();
                }



                //  DrawDefaultInspector();
            }

            EditorGUILayout.EndVertical();

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(M);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}