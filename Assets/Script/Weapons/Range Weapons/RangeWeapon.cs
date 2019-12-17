using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Weapons;

namespace Assets.Weapons
{
    public class RangeWeapon : Weapon
    {
        /// <summary>
        /// 
        /// </summary>
        public new void Attack()
        {
            Debug.Log("Attacking with range weapon");
        }
    }
}