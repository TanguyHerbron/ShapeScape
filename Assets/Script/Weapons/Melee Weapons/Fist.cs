using Assets.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : MeleeWeapon
{
    public float fireRate;

    // Start is called before the first frame update
    void Start()
    {
        Name = "Fist";
        Damage = 1;
    }
}
