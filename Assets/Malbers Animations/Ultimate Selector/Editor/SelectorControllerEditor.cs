using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEditor;
using UnityEditor.Events;
using MalbersAnimations.Events;

namespace MalbersAnimations.Selector
{
    [CustomEditor(typeof(SelectorController))]
    public class SelectorControllerEditor : Editor
    {
        SelectorController M;
        SelectorEditor E;
        MonoScript script;

        private void OnEnable()
        {
            M = (SelectorController)target;
            E = M.GetComponent<SelectorEditor>();
            script = MonoScript.FromMonoBehaviour((SelectorController)target);
            //UnityEventTools.AddPersistentListener<TransformAnimation>((UnityEvent)M.OnItemSelected, M._PlayAnimationTransform);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.BeginVertical(MalbersEditor.StyleBlue);
            EditorGUILayout.HelpBox("All the selector actions and animations are managed here", MessageType.None);
            EditorGUILayout.EndVertical();

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.BeginVertical(MalbersEditor.StyleGray);
            {
                EditorGUI.BeginDisabledGroup(true);
                script = (MonoScript)EditorGUILayout.ObjectField("Script", script, typeof(MonoScript), false);
                EditorGUI.EndDisabledGroup();

                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                M.FocusedItemIndex = EditorGUILayout.IntField(new GUIContent("Focused Item", "Index of the first Item to appear on Focus (Zero Index Based)"), M.FocusedItemIndex);

                if (M.FocusedItemIndex == -1) EditorGUILayout.HelpBox("-1 Means no item is selected", MessageType.Info);
                if (M.FocusedItemIndex < -1 ) M.FocusedItemIndex = - 1;

                    M.RestoreTime = EditorGUILayout.FloatField(new GUIContent("Restore Time", "Time to restore the previuous item to his original position"), M.RestoreTime);
                EditorGUILayout.EndVertical();

                EditorGUILayout.BeginHorizontal();
                M.AnimateSelection = GUILayout.Toggle(M.AnimateSelection,new GUIContent("Animate Selection", "Animate the selection between items"),  EditorStyles.toolbarButton);
                if (M.AnimateSelection) M.SoloSelection = false;
                else M.SoloSelection = true;
                M.SoloSelection = GUILayout.Toggle(M.SoloSelection, new GUIContent("Solo Selection", "Animate the selection between items"), EditorStyles.toolbarButton);
                if (M.SoloSelection) M.AnimateSelection = false;
                else M.AnimateSelection = true;
                EditorGUILayout.EndHorizontal();

                if (M.AnimateSelection)
                {
                    EditorGUILayout.BeginVertical(MalbersEditor.StyleGreen);
                    EditorGUILayout.HelpBox("The Selector Controller will move and rotate to center the selected Item", MessageType.None);
                    EditorGUILayout.EndVertical();


                    EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                    M.SelectionTime = EditorGUILayout.FloatField(new GUIContent("Selection Time", "Time between the selection among the objects"), M.SelectionTime);
                    if (M.SelectionTime < 0) M.SelectionTime = 0; //Don't put time below zero;
                    if (M.SelectionTime != 0)
                        M.SelectionCurve = EditorGUILayout.CurveField(new GUIContent("Selection Curve", "Timing of the selection animation"), M.SelectionCurve);

                    

                    EditorGUILayout.BeginHorizontal();
                    M.DragSpeed = EditorGUILayout.FloatField(new GUIContent("Drag Speed", "Swipe speed when swiping  :)"), M.DragSpeed);

                    if (M.DragSpeed != 0) M.dragHorizontal = GUILayout.Toggle(M.dragHorizontal, new GUIContent(M.dragHorizontal ? "Horizontal" : "Vertical", "Drag/Swipe type from the mouse/touchpad "), EditorStyles.popup);
                    EditorGUILayout.EndHorizontal();
                    if (M.DragSpeed == 0) EditorGUILayout.HelpBox("Drag is disabled", MessageType.Info);
                    EditorGUILayout.EndVertical();

                    if (M.DragSpeed != 0)
                    {
                        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                        M.inertia = EditorGUILayout.Toggle(new GUIContent("Inertia", "Add inertia when Draging is enabled and the mouse is released"), M.inertia);

                        if (M.inertia)
                        {
                            M.inertiaTime = EditorGUILayout.FloatField(new GUIContent("Inertia Time", "The time on inertia when the Drag is released "), M.inertiaTime);
                            M.minInertiaSpeed = EditorGUILayout.FloatField(new GUIContent("Inertia Min Speed", "Min Speed to apply inertia"), M.minInertiaSpeed);
                            M.inertiaCurve = EditorGUILayout.CurveField(new GUIContent("Inertia Curve", "the Curve for the time inertia "), M.inertiaCurve);
                        }
                        EditorGUILayout.EndVertical();

                        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                        M.UseSelectionZone = EditorGUILayout.Toggle(new GUIContent("Use Selection Zone", "Add inertia when Draging is enabled and the mouse is released"), M.UseSelectionZone);

                        if (M.UseSelectionZone)
                        {
                            EditorGUI.BeginChangeCheck();

                            EditorGUILayout.PropertyField(serializedObject.FindProperty("ZMinX"), new GUIContent("Min X", "Region enableed for the Dragging/Swapping"));
                            EditorGUILayout.PropertyField(serializedObject.FindProperty("ZMaxX"), new GUIContent("Max X", "Region enableed for the Dragging/Swapping"));
                            EditorGUILayout.PropertyField(serializedObject.FindProperty("ZMinY"), new GUIContent("Min Y", "Region enableed for the Dragging/Swapping"));
                            EditorGUILayout.PropertyField(serializedObject.FindProperty("ZMaxY"), new GUIContent("Max Y", "Region enableed for the Dragging/Swapping"));
                        }
                        EditorGUILayout.EndVertical();

                    }

                }

                if (M.SoloSelection)
                {
                    EditorGUILayout.BeginVertical(MalbersEditor.StyleGreen);
                    EditorGUILayout.HelpBox("The Selector Controller will not move", MessageType.None);
                    EditorGUILayout.EndVertical();
                    EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                    M.Hover = EditorGUILayout.Toggle(new GUIContent("Hover Selection", "Select by hovering the mouse over an item"), M.Hover);
                    EditorGUILayout.EndVertical();
                }
             

                EditorGUILayout.BeginVertical(EditorStyles.helpBox);

                EditorGUI.indentLevel++;
                M.EditorIdleAnims = EditorGUILayout.Foldout(M.EditorIdleAnims, "Idle Animations");
                EditorGUI.indentLevel--;

                if (M.EditorIdleAnims)
                {
                    EditorGUILayout.BeginHorizontal();
                    M.MoveIdle = GUILayout.Toggle(M.MoveIdle, new GUIContent("Move", "Repeating moving motion for the focused item"), EditorStyles.miniButton);
                    M.RotateIdle = GUILayout.Toggle(M.RotateIdle, new GUIContent("Rotate", "Turning table for the focused item"), EditorStyles.miniButton);
                    M.ScaleIdle = GUILayout.Toggle(M.ScaleIdle, new GUIContent("Scale", "Repeating scale motion for the focused item"), EditorStyles.miniButton);
                    EditorGUILayout.EndHorizontal();

                    if (M.MoveIdle)
                    {
                        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                        M.MoveIdleAnim = (TransformAnimation)EditorGUILayout.ObjectField(new GUIContent("Move Idle", "Idle Move Animation when is on focus"), M.MoveIdleAnim, typeof(TransformAnimation), false);
                        EditorGUILayout.EndVertical();
                    }
                    if (M.RotateIdle)
                    {
                        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                        M.ItemRotationSpeed = EditorGUILayout.FloatField(new GUIContent("Speed", "How fast the focused Item will rotate"), M.ItemRotationSpeed);
                        M.TurnTableVector = EditorGUILayout.Vector3Field(new GUIContent("Rotation Vector", "Choose your desire vector to rotate around"), M.TurnTableVector);
                        EditorGUILayout.EndVertical();
                    }

                    if (M.ScaleIdle)
                    {
                        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                        M.ScaleIdleAnim = (TransformAnimation)EditorGUILayout.ObjectField(new GUIContent("Scale Idle", "Idle Scale Animation when is on focus"), M.ScaleIdleAnim, typeof(TransformAnimation), false);
                        EditorGUILayout.EndVertical();
                    }
                }
                EditorGUILayout.EndVertical();

                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                M.LockMaterial = (Material)EditorGUILayout.ObjectField(new GUIContent("Lock Material", "Material choosed for the locked objects"), M.LockMaterial, typeof(Material), false);
                EditorGUILayout.BeginHorizontal();
                EditorGUIUtility.labelWidth = 95;

                if (E && E.SelectorCamera)
                {
                    M.frame_Camera = EditorGUILayout.Toggle(new GUIContent("Frame Camera", " Auto Adjust the camera position by the size of the object"), M.frame_Camera, GUILayout.MinWidth(20));
                    if (M.frame_Camera)
                    {
                        EditorGUIUtility.labelWidth = 55;
                        M.frame_Multiplier = EditorGUILayout.FloatField(new GUIContent("Multiplier", "Distance Mupltiplier for the camera frame"), M.frame_Multiplier, GUILayout.MaxWidth(100));
                    }
                }
                    EditorGUIUtility.labelWidth = 0;
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();

                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                EditorGUI.indentLevel++;
                M.EditorInput = EditorGUILayout.Foldout(M.EditorInput, "Inputs");
                EditorGUI.indentLevel--;

                if (M.EditorInput)
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("Submit"), new GUIContent("Submit"));

                    EditorGUILayout.PropertyField(serializedObject.FindProperty("ChangeLeft"), new GUIContent("Change Left"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("ChangeRight"), new GUIContent("Change Right"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("ChangeUp"), new GUIContent("Change Up"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("ChangeDown"), new GUIContent("Change Down"));
                }
                EditorGUILayout.EndVertical();

                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                EditorGUI.indentLevel++;
                M.EditorAdvanced = EditorGUILayout.Foldout(M.EditorAdvanced, "Advanced");
              
                if (M.EditorAdvanced)
                {
                    if (!M.Hover) M.ClickToFocus = EditorGUILayout.Toggle(new GUIContent("Click to Focus", "If a another item is touched/clicked, focus on it"), M.ClickToFocus);
                    M.ChangeOnEmptySpace = EditorGUILayout.Toggle(new GUIContent("Change on Empty Space", "If there's a Click/Touch on an empty space change to the next/previous item"), M.ChangeOnEmptySpace);
                    M.Threshold = EditorGUILayout.FloatField(new GUIContent("Threshold", "Max Threshold to identify if is a click/touch or a drag/swipe"), M.Threshold);
                }
                EditorGUI.indentLevel--;
                EditorGUILayout.EndVertical();

                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                EditorGUI.indentLevel++;
                M.EditorShowEvents = EditorGUILayout.Foldout(M.EditorShowEvents, "Events");
                EditorGUI.indentLevel--;

                if (M.EditorShowEvents)
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("OnClickOnItem"), new GUIContent("On Click/Touch an Item"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("OnItemFocused"), new GUIContent("On Item Focused"));
                    if (M.AnimateSelection)
                    {
                      EditorGUILayout.PropertyField(serializedObject.FindProperty("OnIsChangingItem"), new GUIContent("Is Changing Item"));
                    }
                }
                EditorGUILayout.EndVertical();

              
            }
            EditorGUILayout.EndVertical();


            if (EditorGUI.EndChangeCheck())
            {
                Undo.RegisterCompleteObjectUndo(M, "Controller Values Changed");
                EditorUtility.SetDirty(target);
            }
            serializedObject.ApplyModifiedProperties();
        }
        
    }
}