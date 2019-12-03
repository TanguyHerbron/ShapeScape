using Assets.Entities;
using System.Collections;
using UnityEngine;

public class Player : Character
{
    public int speed;

    // Start is called before the first frame update
    void Start()
    {
        Name = "Player";
        HP = 6;
        Speed = speed;
    }

    /// <summary>
    /// Collision detector
    /// Checks if the object colliding with the player should or not apply damage
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if((collision.gameObject.CompareTag("Melee Weapon") || collision.gameObject.CompareTag("Ennemy")) && !Invicible)
        {
            ApplyDamage(1);

            if(IsDead())
            {
                GameObject.Find("Canvas").transform.Find("DeathPanel").gameObject.SetActive(true);
            } else
            {
                StartCoroutine(Invicibility());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        RoomSpawn spawn = GameObject.Find("Grid").GetComponent<RoomSpawn>();

        if(collision.name == "End")
        {
            GameObject.Find("Canvas").transform.Find("EndPanel").gameObject.SetActive(true);
        }

        if(collision.name.Split(',').Length == 4)
        {
            string[] coords = collision.name.Split(',');

            spawn.SpawnEnnemies(System.Convert.ToInt32(coords[0]),
                                System.Convert.ToInt32(coords[2]),
                                System.Convert.ToInt32(coords[1]),
                                System.Convert.ToInt32(coords[3]));
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
        yield return new WaitForSeconds(1);
        Invicible = false;
    }
}
