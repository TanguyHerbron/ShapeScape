using Assets.Entities;
using System.Collections;
using UnityEngine;

/// <summary>
/// Class defining the behavour of the player
/// </summary>
public class Player : Character
{
    public int speed;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        Name = "Player";
        HP = 6;
        Speed = speed;

        animator = this.GetComponent<Animator>();
    }
    
    // Start is called before the first frame update
    void Update()
    {
        animator.SetBool("Invincible", Invicible);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        RoomSpawn spawn = GameObject.Find("Grid").GetComponent<RoomSpawn>();

        if ( collision.name == "End")
        {
            GameObject.Find("Canvas").transform.Find("EndPanel").gameObject.SetActive(true);
            Time.timeScale = 0.0f;
        }

        //spawns ennemies
        if ( collision.name.Split(',').Length == 5 )
        {
            string[] coords = collision.name.Split(',');

            if ( coords[4].Equals(" ") )
            {
                spawn.SpawnEnnemies(System.Convert.ToInt32(coords[0]),
                                    System.Convert.ToInt32(coords[2]),
                                    System.Convert.ToInt32(coords[1]),
                                    System.Convert.ToInt32(coords[3]));

                collision.name = collision.name.Replace(' ', 'c');
            }
        }
    }
}
