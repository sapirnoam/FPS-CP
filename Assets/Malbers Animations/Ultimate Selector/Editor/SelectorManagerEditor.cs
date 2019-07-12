using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEditor;
using UnityEditor.Events;
using MalbersAnimations.Events;

namespace MalbersAnimations.Selector
{
    [CustomEditor(typeof(SelectorManager))]
    public class SelectorManagerEditor : Editor
    {
        SelectorManager M;
        MonoScript script;


        bool DataHelp = false;
        bool EventHelp = false;

        private void OnEnable()
        {
            M = (SelectorManager)target;
            script = MonoScript.FromMonoBehaviour((SelectorManager)target);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.BeginVertical(MalbersEditor.StyleBlue);
            EditorGUILayout.HelpBox("Global Manager\nThe selector uses UI LAYER to work properly", MessageType.None);
            EditorGUILayout.EndVertical();

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.BeginVertical(MalbersEditor.StyleGray);
            {
                EditorGUI.BeginDisabledGroup(true);
                script = (MonoScript)EditorGUILayout.ObjectField("Script", script, typeof(MonoScript), false);
                EditorGUI.EndDisabledGroup();

                EditorGUILayout.BeginVertical(EditorStyles.helpBox);

                EditorGUILayout.PropertyField(serializedObject.FindProperty("Input"), new GUIContent("Input", "Toogle the Selector On and Off"));
                if (!Application.isPlaying)
                {
                    M.enableSelector = EditorGUILayout.ToggleLeft(new GUIContent(M.enableSelector ? "Open on Awake" : "Closed on Awake", "Initial State of the selector"), M.enableSelector, GUILayout.MinWidth(80));
                }
                else
                {
                    EditorGUILayout.LabelField(M.enableSelector ? "OPEN" : "CLOSED", EditorStyles.largeLabel);
                }


                M.DontDestroy = EditorGUILayout.ToggleLeft(new GUIContent("Don't destroy on Load", "Set the Selector to not be destroyed automatically when loading a new scene."), M.DontDestroy);

                EditorGUILayout.EndVertical();


                if (Application.isPlaying)
                {
                    EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                    EditorGUI.BeginDisabledGroup(true);
                    M.ItemSelected = (MItem)EditorGUILayout.ObjectField(new GUIContent("Item", "Item Holder"), M.ItemSelected, typeof(MItem), true);
                    M.OriginalItemSelected = (GameObject)EditorGUILayout.ObjectField(new GUIContent("Original GO", "Original Game Object"), M.OriginalItemSelected, typeof(GameObject), true);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.EndVertical();
                }


                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                EditorGUILayout.BeginHorizontal();
                M.InstantiateItems = EditorGUILayout.ToggleLeft(new GUIContent("Instantiate Item", "Insantiate the original prefab of the selected item"), M.InstantiateItems, GUILayout.MinWidth(80));

                if (M.InstantiateItems)
                {
                    M.SpawnPoint = (Transform)EditorGUILayout.ObjectField(new GUIContent("", "Spawn Point to Instantiate the items, if empty it will spawn it into (0,0,0)"), M.SpawnPoint, typeof(Transform), true, GUILayout.MinWidth(80));
                }
                EditorGUILayout.EndHorizontal();
                if (M.InstantiateItems)
                {
                    M.RemoveLast = EditorGUILayout.ToggleLeft(new GUIContent("Remove Last Spawn", "Remove the Last Spawned Object"), M.RemoveLast);
                    M.DontDestroySelectedItem = EditorGUILayout.ToggleLeft(new GUIContent("Don't destroy Selected Item", "Makes the Selected Item not be destroyed automatically when loading a new scene."), M.DontDestroySelectedItem);
                }
                EditorGUILayout.EndVertical();

                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                EditorGUI.indentLevel++;
                M.ShowAnims = EditorGUILayout.Foldout(M.ShowAnims, "Animate Open/Close");
                EditorGUI.indentLevel--;

                if (M.ShowAnims)
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("Target"), new GUIContent("Target to Animate", "This is the Transform to animate In and Out"), GUILayout.MinWidth(50));

                    if (M.Target)
                    {
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("EnterAnimation"), new GUIContent("Open", "Plays an animation on enter"), GUILayout.MinWidth(50));
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("ExitAnimation"), new GUIContent("Close", "Plays an animation on exit"), GUILayout.MinWidth(50));
                    }
                }

                EditorGUILayout.EndVertical();

                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                EditorGUILayout.BeginHorizontal();
                M.Data = (SelectorData)EditorGUILayout.ObjectField(new GUIContent("Data", "This is an Scriptable Object to save all the important values of the Selector\n You can enable 'Use PlayerPref' and save the data there, but is recommended to use a Better and secure  'Saving System'"), M.Data, typeof(SelectorData), false);
                DataHelp = GUILayout.Toggle(DataHelp, "?", EditorStyles.miniButton, GUILayout.MaxWidth(16));
                EditorGUILayout.EndHorizontal();

                if (M.Data)
                {
                    M.Data.Save.Coins = EditorGUILayout.IntField(new GUIContent("Coins", "Amount of coins"), M.Data.Save.Coins);
                    M.Data.usePlayerPref = EditorGUILayout.ToggleLeft(new GUIContent("Use PlayerPref to Save Data", "Enable it to persistent save the Data using PlayerPref Class, but is recommended to use a better and secure 'Saving System' and connect it to the Data Asset"), M.Data.usePlayerPref);
                }

                if (DataHelp)
                {
                    EditorGUILayout.HelpBox("To create a Data Asset go to\nAssets -> Create -> MalbersAnimations -> Ultimate Selector -> SelectorData\n\nThe Data are 'Scriptable Objects Assets' used to save all the important values of the Selector, like Coins, Locked Items, Item Amount.\nYou can enable 'Use PlayerPref' to persistent save the Data using that method, but is recomended to use a better and secure 'Saving System' and connect it to the Data Asset", MessageType.None);
                }
                EditorGUILayout.BeginHorizontal();

                if (M.Data)
                {
                    if (GUILayout.Button(new GUIContent("Save Default Data", "Store all the Items Values and Coins as the Restore/Default Data")))
                    {
                        M.SaveDefaultData();
                        if (M.Data)
                        {
                            EditorUtility.SetDirty(M.Data);
                        }
                    }

                    if (GUILayout.Button(new GUIContent("Restore Data", "Restore all the values from  to the Default Data")))
                    {
                        M.RestoreToDefaultData();
                        if (M.Data)
                        {
                            EditorUtility.SetDirty(M.Data);
                        }
                    }
                }

                EditorGUILayout.EndHorizontal();

                EditorGUILayout.EndVertical();

                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                EditorGUI.indentLevel++;
                EditorGUILayout.BeginHorizontal();
                M.ShowEvents = EditorGUILayout.Foldout(M.ShowEvents, "Events");
                EventHelp = GUILayout.Toggle(EventHelp, "?", EditorStyles.miniButton, GUILayout.MaxWidth(16));
                EditorGUILayout.EndHorizontal();
                EditorGUI.indentLevel--;
                if (M.ShowEvents)
                {
                    if (EventHelp)
                    {
                        EditorGUILayout.HelpBox("On Selected: Invoked when an Item is selected using the method: 'SelectItem()'\n\nBy Default the 'Select' button on the UI is the ones who calls 'SelectItem()'\n\nOn Open: Invoked when the selector is open\n\nOn Closed: Invoked when the selector is closedn\n\nOn Data Changed: Invoked when the Selector Data is modified \n\nYou can conect whatever you want to your own system using this unity event\n\nIf the Item has an Original GameObject (OGo), the OGo will be the one sent on the Event, if not the Item gameObject will be the one sent on the Event", MessageType.None);
                    }
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("OnSelected"), new GUIContent("On Selected"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("OnOpen"), new GUIContent("On Open"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("OnClosed"), new GUIContent("On Closed"));
                    if (M.Data != null)
                    {
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("OnDataChanged"), new GUIContent("On Data Changed"));
                    }
                }
                EditorGUILayout.EndVertical();
            }
          
            EditorGUILayout.EndVertical();

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RegisterCompleteObjectUndo(M, "Manager Values Changed");
                EditorUtility.SetDirty(target);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
