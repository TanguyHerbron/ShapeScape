using Assets.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public Weapon weapon;
    private Weapon oldWeapon;

    // Update is called once per frame
    void Update()
    {
        if ( weapon != oldWeapon )
        {
            weapon = Instantiate(weapon, transform.position + weapon.transform.position, weapon.transform.rotation);
            weapon.transform.parent = transform;
            Destroy(oldWeapon);

            oldWeapon = weapon;
        }

        if (weapon.GetComponentInChildren<TrailRenderer>() && weapon.GetComponent<Animator>())
        {
            weapon.GetComponentInChildren<TrailRenderer>().enabled = weapon.GetComponent<Animator>().GetBool("Swing");
        }
    }

    /// <summary>
    /// Switch the weapon in hand
    /// </summary>
    /// <param name="newWeapon">The weapon to put in the hand of the player</param>
    public void SwitchWeapon(Weapon newWeapon)
    {
        weapon = newWeapon;
    }
}
