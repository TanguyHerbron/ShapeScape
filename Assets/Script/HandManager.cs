using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public GameObject weapon;
    private GameObject oldWeapon;

    // Update is called once per frame
    void Update()
    {
        if(weapon != oldWeapon)
        {
            weapon = Instantiate(weapon, transform.position + weapon.transform.position, weapon.transform.rotation);
            weapon.transform.parent = transform;
            Destroy(oldWeapon);

            oldWeapon = weapon;
        }
    }

    public void attack()
    {
        Animator animator = weapon.GetComponent<Animator>();

        animator.SetBool("Swing", true);
    }
}
