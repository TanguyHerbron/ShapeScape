﻿using UnityEngine;

namespace Assets.Weapons
{
    /// <summary>
    /// Abstract class for every weapons in the game
    /// </summary>
    public class Weapon : MonoBehaviour, IWeapon
    {
        private int id;
        private string name;
        private float damage;
        private float fireRate;
        private float range;
        private int ammo;
        private bool isAOE = false;

        /***
            METHODS
        ***/

        public virtual void Attack()
        {
            Debug.Log("suuuuuuuup");
        }

        /***
            GETTERS AND SETTERS
        ***/

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public int ID
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public float Damage
        {
            get
            {
                return damage;
            }

            set
            {
                damage = value;
            }
        }

        public float FireRate
        {
            get
            {
                return fireRate;
            }

            set
            {
                fireRate = value;
            }
        }

        public float Range
        {
            get
            {
                return range;
            }

            set
            {
                range = value;
            }
        }

        public bool IsAOE
        {
            get
            {
                return isAOE;
            }

            set
            {
                isAOE = value;
            }
        }
    }
}