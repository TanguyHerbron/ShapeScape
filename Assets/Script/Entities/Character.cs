using Assets.Weapons;
using System.Collections;
using UnityEngine;

namespace Assets.Entities
{
    public class Character : Entity
    {
        /// <summary>
        /// Collision detector
        /// Checks if the object colliding with the player should or not apply damage
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if ( ( collision.gameObject.CompareTag("Melee Weapon") || collision.gameObject.CompareTag("Ennemy") ) && !Invicible )
            {
                ApplyDamage(1);

                if ( IsDead() )
                {
                    GameObject.Find("Canvas").transform.Find("DeathPanel").gameObject.SetActive(true);
                    Time.timeScale = 0.0f;
                }
                else
                {
                    StartCoroutine(Invicibility());

                    if (collision.gameObject.CompareTag("Ennemy"))
                    {
                        ScreenShakeController.instance.StartShake(.6f, .3f);
                    }
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Weapon"))
            {
                Weapon weapon = collision.gameObject.GetComponent<Weapon>();
                ApplyDamage(weapon.Damage);

                if (IsDead())
                {
                    GameObject.Find("Canvas").transform.Find("DeathPanel").gameObject.SetActive(true);
                    Time.timeScale = 0.0f;
                }
                else
                {
                    StartCoroutine(Invicibility());
                }
            }
        }

        /// <summary>
        /// Provides immunity to the player for one second
        /// By setting the immune boolean to true
        /// </summary>
        /// <returns></returns>
        private IEnumerator Invicibility()
        {
            Invicible = true;
            yield return new WaitForSeconds(2);
            Invicible = false;
        }
    }
}

