using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary> A ScriptableObject container to store and change the game controls. 
/// <para>Could be swapped out as profiles, maybe.</para>
/// <para>This is mostly intended to be changed through the Options, but it could also be set through the Inspector.</para>
/// </summary>
[CreateAssetMenu(menuName ="Global Objects/Controls")]
public sealed class Controls : ScriptableObject
{

    public List<Control> controlList = new List<Control>();

    /// <summary>
    /// Duplicates the Controls object along with the entire list, and returns a new object.
    /// </summary>
    public Controls Duplicate()
    {
        Controls newControls = new Controls();
        newControls.controlList = controlList;

        return newControls;
    }

    /// <summary>
    /// Returns the name string of a control that uses the specified Key
    /// </summary>
    /// <param name="key">KeyCode to search by.</param>
    /// <returns>The name of the control, if found.</returns>
    public string FindControlByKey(KeyCode key)
    {
        foreach (Control control in controlList)
        {
            if (control.key == key)
            {
                return control.name;
            }
        }

        throw new Exception("Control type not found! Check your spelling, or consult the list again.");
    }

    /// <summary>
    /// Returns a Key if given the right name for a control type.
    /// </summary>
    /// <param name="name">Control name to search.</param>
    /// <returns>The KeyCode of the Control, if found.</returns>
    public KeyCode FindControlByName(string name)
    {
        foreach (Control control in controlList)
        {
            if (control.name == name)
            {
                return control.key;
            }
        }

        throw new Exception("Control type not found! Check your spelling, or consult the list again.");
    }

}

/// <summary>
/// A generic class to hold the name-key pairs for the Controls class.
/// </summary>
[Serializable]
public sealed class Control
{
    public string name;
    public KeyCode key;
}