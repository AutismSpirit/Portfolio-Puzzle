using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ControlsManagerWindow : EditorWindow
{

    public Controls controlSet;

    [MenuItem("Custom/Controls Manager")]
    static void Init()
    {
        ControlsManagerWindow window = (ControlsManagerWindow)EditorWindow.GetWindow(typeof(ControlsManagerWindow));
        window.Show();
    }

    // This function is called when the object becomes enabled and active
    private void OnEnable()
    {

    }

    // Implement your own editor GUI here
    private void OnGUI()
    {

    }

    // This function is called when the script is loaded or a value is changed in the inspector (Called in the editor only)
    private void OnValidate()
    {

    }

}
