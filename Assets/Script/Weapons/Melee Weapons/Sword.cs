using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Weapons
{
    public class Sword : MeleeWeapon
    {
        public float fireRate;
        private float timeStamp;

        // Start is called before the first frame update
        void Start()
        {
            Name = "Old School";
            FireRate = fireRate;
            timeStamp = Time.time;
            Ammo = 1;
            MagazineSize = 1;
            TotalAmmo = 1;
            Damage = 3;
            Sprite = this.GetComponent<SpriteRenderer>().sprite;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public override void Attack()
        {
            if( timeStamp + fireRate < Time.time )
            {
                Animator animator = this.GetComponent<Animator>();
                animator.SetBool("Swing", true);
                timeStamp = Time.time;
            }
            
        }
    }
}
