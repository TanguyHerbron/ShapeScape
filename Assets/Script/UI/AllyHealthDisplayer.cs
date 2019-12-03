using Assets.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyHealthDisplayer : MonoBehaviour
{
    private Character ally;
    private SpriteRenderer spriteRenderer;

    public Sprite[] allySprites;

    private float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        ally = this.GetComponent<Coward>();
        currentHealth = ally.HP;

        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if( currentHealth > ally.HP )
        {
            spriteRenderer.sprite = allySprites[(int) ally.HP - 1];
            currentHealth = ally.HP;
            Debug.Log(ally.HP);
        }
    }
}
