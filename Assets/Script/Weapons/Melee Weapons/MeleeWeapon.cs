using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Weapons;

namespace Assets.Weapons
{
    public class MeleeWeapon : Weapon
    {
        /// <summary>
        /// 
        /// </summary>
        public override void Attack()
        {
            Debug.Log("Attacking with melee weapon");
        }
    }
}
