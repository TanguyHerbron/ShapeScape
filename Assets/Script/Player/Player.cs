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


}
