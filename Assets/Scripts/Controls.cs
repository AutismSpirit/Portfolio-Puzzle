using System;
using System.Collections.Generic;
using UnityEngine;

//This ScriptableObject basically acts like a Dictionary wrapper, made because Dictionaries aren't serializable in the Unity Inspector.

//Create a new one for a new set of controls, and change it through the options menu.
//When the player changes the Default set of controls for the first time, Duplicate it and change it out with a new custom set, and serialize it into savedata.


/// <summary> A ScriptableObject container to store and change the game controls. 
/// <para>Could be swapped out as profiles, maybe.</para>
/// <para>This is mostly intended to be changed through the Options, but it could also be set through the Inspector.</para>
/// </summary>
[CreateAssetMenu(menuName = "Global Objects/Controls")]
public sealed class Controls : ScriptableObject
{

    public List<KeyControl> controlList;
    public List<AxisControl> axisList;

    private Dictionary<string, KeyCode[,]> axisMap = new Dictionary<string, KeyCode[,]>();
    private Dictionary<string, KeyCode> keyMap = new Dictionary<string, KeyCode>();

    /// <summary>
    /// Called from the Game Controller on startup.
    /// </summary>
    public void Initialize()
    {
        Debug.Log("Initializing controls...");
        for (int i = 0; i < controlList.Count - 1; i++)
        {
            keyMap.Add(controlList[i].name, controlList[i].key);
        }

        //for (int i = 0; i < axisList.Count - 1; i++)
        //{
        //    axisList[i].keys.SetValue(axisList[i].negKey, i, 0);
        //    axisList[i].keys.SetValue(axisList[i].posKey, i, 1);

        //    axisMap.Add(axisList[i].name, axisList[i].keys);
        //}
    }

    /// <summary>
    /// Duplicates the Controls object along with the entire list, and returns a new object.
    /// </summary>
    public Controls Duplicate()
    {
        Controls newControls = new Controls();
        newControls.controlList = controlList;
        newControls.Initialize();

        return newControls;
    }

    /// <summary>
    /// Returns true if the key specified in the current Control scheme was pressed down on this frame.
    /// </summary>
    public bool GetKeyDown(string keyName)
    {
        KeyCode key;
        keyMap.TryGetValue(keyName, out key);

        return Input.GetKeyDown(key);
    }

    /// <summary>
    /// Returns true if the key specified in the current Control scheme is pressed on this frame.
    /// </summary>
    public bool GetKey(string keyName)
    {
        KeyCode key;
        keyMap.TryGetValue(keyName, out key);

        return Input.GetKey(key);
    }

    /// <summary>
    /// Returns true if the key specified in the current Control scheme is released on this frame.
    /// </summary>
    public bool GetKeyUp(string keyName)
    {
        KeyCode key;
        keyMap.TryGetValue(keyName, out key);

        return Input.GetKeyUp(key);
    }

    public int GetAxis(string axisName)
    {
        KeyCode[,] value;
        bool neg = false, pos = false;
        axisMap.TryGetValue(axisName, out value);


        if (Input.GetKey(value[0,0]))
        {
            neg = true;
        }

        if (Input.GetKey(value[0,1]))
        {
            pos = true;
        }

        if (neg)
        {
            return -1;
        }
        if (pos)
        {
            return 1;
        }
        if (neg && pos)
        {
            return 0;
        }

        return 0;
    }

    /// <summary>
    /// Returns the name string of a control that uses the specified Key
    /// </summary>
    /// <param name="key">KeyCode to search by.</param>
    /// <returns>The name of the control, if found.</returns>
    public string FindControlByKey(KeyCode key)
    {
        foreach (KeyControl control in controlList)
        {
            if (control.key == key)
            {
                return control.name;
            }
        }

        throw new Exception("KeyControl type not found! Check your spelling, or consult the list again.");
    }

    /// <summary>
    /// Returns a Key if given the right name for a control type.
    /// </summary>
    /// <param name="name">Control name to search.</param>
    /// <returns>The KeyCode of the Control, if found.</returns>
    public KeyCode FindControlByName(string name)
    {
        foreach (KeyControl control in controlList)
        {
            if (control.name == name)
            {
                return control.key;
            }
        }

        throw new Exception("KeyControl type not found! Check your spelling, or consult the list again.");
    }

}

/// <summary>
/// A generic class to hold the name-key pairs for the Controls class.
/// </summary>
[Serializable]
public sealed class KeyControl
{
    public string name;
    public KeyCode key;
}

/// <summary>
/// A generic class to hold names and key-key pairs of Axes for the Controls class.
/// </summary>
[Serializable]
public sealed class AxisControl : IEquatable<AxisControl>
{
    public string name;
    public KeyCode negKey;
    public KeyCode posKey;

    public KeyCode[,] keys;

    public override bool Equals(object obj)
    {
        if (obj == null || !(obj is AxisControl))
            return false;

        return Equals((AxisControl)obj);
    }

    public bool Equals(AxisControl other)
    {
        return name == other.name && negKey == other.negKey && posKey == other.posKey;
    }

    public override int GetHashCode()
    {
        int hashCode = -176869778;
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name);
        hashCode = hashCode * -1521134295 + negKey.GetHashCode();
        hashCode = hashCode * -1521134295 + posKey.GetHashCode();
        return hashCode;
    }

    public override string ToString()
    {
        return $"{name}, negative value is {negKey}, positive value is {posKey}";
    }
}