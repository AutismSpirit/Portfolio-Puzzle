using UnityEngine;

/// <summary>
/// Expandable weapons system for making new types of usable weapons as Prefabs.
/// </summary>
public class Weapon : MonoBehaviour
{
    [Header("Basic Weapon Setup")]
    [Tooltip("The amount of maximum ammo the weapon has. If set to 0, the ammo is infinite.")]
    public int ammo;

    private int _currentAmmo;
    private int CurrentAmmo
    {
        get
        {
            return _currentAmmo;
        }

        set
        {
            _currentAmmo = value;
            if (_currentAmmo < 0)
            {
                if (ammo != 0)
                {
                    OnReload();
                }
            }
        }
    }

    [Tooltip("The reload time, in seconds, if the weapon has ammo.")]
    public float reloadTime;

    public void OnRightClick()
    {

    }

    public void OnFire()
    {

    }

    private void OnReload()
    {

    }

}
