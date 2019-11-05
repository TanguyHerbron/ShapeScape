using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Weapons;

public class RangeWeapon : Weapon
{
    private int ammo;

    public int Ammo
    {
        get
        {
            return ammo;
        }

        set
        {
            ammo = value;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void Shoot()
    {
        // TODO: Implement
    }
}
