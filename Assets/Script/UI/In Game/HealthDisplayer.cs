using Assets.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplayer : MonoBehaviour
{
    private Character player;

    private int currentNumOfHearts;

    public Image[] hearts;

    public Sprite fullHeart;
    public Sprite halfHeart;


    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        
        // Checking current number of heart
        currentNumOfHearts = (int) Math.Ceiling(( (double) player.HP ) / 2);

        // Displaying hearts in the array if their index is lower than the player's HP 
        for ( int i = 0; i < hearts.Length; i++ )
        {
            if(i < currentNumOfHearts)
            {
                hearts[i].enabled = true;

                // Displaying a full or half heart for the  last heart
                if( i+1 == currentNumOfHearts )
                {
                    // Whole number
                    if ( player.HP % 2 == 0 )
                    {
                        hearts[i].sprite = fullHeart;
                    }
                    // Not whole
                    else
                    {
                        hearts[i].sprite = halfHeart;
                    }
                }
            }
            else
            {
                hearts[i].enabled = false;
            }



        }

    }
}
