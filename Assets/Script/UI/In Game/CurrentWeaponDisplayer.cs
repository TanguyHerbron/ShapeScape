using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentWeaponDisplayer : MonoBehaviour
{
    public Text weaponNameTxt;
    public Text weaponInfoTxt;
    public Image weaponImage;
    private HandManager hand;

    // Start is called before the first frame update
    void Start()
    {
        hand = GameObject.FindGameObjectWithTag("Player").GetComponent<HandManager>();

        weaponNameTxt.text = "No Weapon";
        weaponInfoTxt.text = hand.weapon.Ammo + "/" + hand.weapon.MagazineSize;
    }

    // Update is called once per frame
    void Update()
    {
        hand = GameObject.FindGameObjectWithTag("Player").GetComponent<HandManager>();
        weaponNameTxt.text = hand.weapon.Name;
        weaponInfoTxt.text = hand.weapon.Ammo + "/" + hand.weapon.MagazineSize;
        weaponImage.sprite = hand.weapon.Sprite;
        weaponImage.preserveAspect = true;
    }
}
