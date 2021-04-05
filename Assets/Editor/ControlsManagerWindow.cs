using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ControlsManagerWindow : EditorWindow
{
    //control file!
    bool showControls;
    bool showKeys;
    bool showAxes;

    string status = "Edit the control scheme";

    public Controls controlSet;

    KeyControl latestAddition;
    AxisControl latestAxisAddition;

    [MenuItem("Custom/Controls Manager")]
    static void Init()
    {
        ControlsManagerWindow window = (ControlsManagerWindow)EditorWindow.GetWindow(typeof(ControlsManagerWindow));
        window.Show();
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        controlSet = (Controls)EditorGUILayout.ObjectField(controlSet, typeof(Controls), false);
        EditorGUILayout.EndHorizontal();

        showControls = EditorGUILayout.Foldout(showControls, status);
        if (showControls)
        {
            if (controlSet == null)
            {
                status = "Plug in a Controls object!";
            }
            else
            {
                showKeys = EditorGUILayout.Foldout(showKeys, "Show the single key controls");

                if (showKeys)
                {
                    EditorGUILayout.BeginHorizontal();
                    if (GUILayout.Button("Add New"))
                    {
                        if (controlSet.controlList.Count == 0)
                        {
                            controlSet.controlList.Add(new KeyControl());
                        }

                        latestAddition = new KeyControl();
                        controlSet.controlList.Add(latestAddition);
                    }
                    if (GUILayout.Button("Undo Add/Delete"))
                    {
                        if (latestAddition != null)
                        {
                            controlSet.controlList.Remove(latestAddition);
                            latestAddition = null;
                        }
                        else
                        {
                            controlSet.controlList.Remove(controlSet.controlList[controlSet.controlList.Count - 1]);
                        }
                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.Space();
                    
                    EditorGUILayout.Space();

                    foreach (KeyControl control in controlSet.controlList)
                    {
                        EditorGUILayout.BeginHorizontal();
                        control.key = (KeyCode)EditorGUILayout.EnumPopup(control.key);
                        control.name = EditorGUILayout.TextField(control.name);
                        EditorGUILayout.EndHorizontal();
                        EditorGUILayout.Space();
                    }
                }

                showAxes = EditorGUILayout.Foldout(showAxes, "Show the axis controls");

                if (showAxes)
                {
                    EditorGUILayout.BeginHorizontal();
                    if (GUILayout.Button("Add New"))
                    {
                        if (controlSet.axisList.Count == 0)
                        {
                            controlSet.axisList.Add(new AxisControl());
                        }

                        latestAxisAddition = new AxisControl();
                        controlSet.axisList.Add(latestAxisAddition);
                    }
                    if (GUILayout.Button("Undo Add/Delete"))
                    {
                        if (latestAxisAddition != null)
                        {
                            controlSet.axisList.Remove(latestAxisAddition);
                        }
                        else
                        {
                            controlSet.axisList.Remove(controlSet.axisList[controlSet.axisList.Count - 1]);
                        }
                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.Space();
                    EditorGUILayout.Space();

                    foreach (AxisControl axis in controlSet.axisList)
                    {
                        EditorGUILayout.BeginHorizontal();
                        axis.name = EditorGUILayout.TextField(axis.name);
                        axis.negKey = (KeyCode)EditorGUILayout.EnumPopup(axis.negKey);
                        axis.posKey = (KeyCode)EditorGUILayout.EnumPopup(axis.posKey);
                        EditorGUILayout.EndHorizontal();
                    }

                }

                status = "Edit the control scheme";

            }
        }
    }

}
