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
            FireRate = fireRate;
            timeStamp = Time.time;
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
