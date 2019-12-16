using UnityEngine;

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
        private int totalAmmo;
        private int magazineSize;
        private bool isAOE = false;
        private Sprite sprite;

        /***
            METHODS
        ***/


        public virtual void Attack()
        {
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

        public int Ammo { get => ammo; set => ammo= value; }
        public int TotalAmmo { get => totalAmmo; set => totalAmmo= value; }
        public int MagazineSize { get => magazineSize; set => magazineSize= value; }
        public Sprite Sprite { get => sprite; set => sprite= value; }
    }
}