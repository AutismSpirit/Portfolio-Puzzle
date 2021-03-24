using UnityEngine;

/// <summary>
/// Expandable weapons system for making new types of usable weapons as Prefabs.
/// </summary>
public class Weapon : MonoBehaviour
{

    [Header("Basic Weapon Setup")]
    [Tooltip("The amount of maximum ammo the weapon has. If set to 0, the ammo is infinite.")]
    public int ammo;

    /// <summary>
    /// Returns true if the weapon uses ammo. False if it's infinite.
    /// </summary>
    public bool UsesAmmo
    {
        get
        {
            return ammo != 0 ? true : false;
        }
    }

    public bool autoReload;

    private int _currentAmmo;

    /// <summary>
    /// A container for the current ammo in the weapon. Optionally calls the reload function if depleted.
    /// </summary>
    public int CurrentAmmo
    {
        get
        {
            return _currentAmmo;
        }

        set
        {
            _currentAmmo = value;
            if (!autoReload)
            {
                if (_currentAmmo < 0)
                {
                    if (ammo != 0)
                    {
                        OnReload();
                        _currentAmmo = ammo;
                    }
                }
            }
            else
            {
                Mathf.Clamp(_currentAmmo, 0, ammo);
            }
        }
    }

    public float range;

    [Tooltip("The reload time, in seconds, if the weapon has ammo.")]
    public float reloadTime;

    /// <summary>
    /// Behaviour on alt fire.
    /// </summary>
    public virtual void OnRightClick()
    {

    }

    /// <summary>
    /// Behaviour on fire.
    /// </summary>
    public virtual void OnFire()
    {

    }

    /// <summary>
    /// Behaviour on pressing the reload key, or depleting the ammo if AutoReload is on.
    /// </summary>
    public virtual void OnReload()
    {

    }

}
