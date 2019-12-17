using UnityEngine;

namespace Assets.Weapons
{
    public class Sword : MeleeWeapon
    {
        public float fireRate;

        // Start is called before the first frame update
        void Start()
        {
            Name = "Old School";
            FireRate = fireRate;
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
                Animator animator = this.GetComponent<Animator>();
                animator.SetTrigger("Swing");
        }
    }
}
