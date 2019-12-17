using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public bool roomEntered;

    public void Start()
    {
        roomEntered = false;
    }

    /// <summary>
    /// Checks if the player enters the room.
    /// If the player enters then we spawn ennemies
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        RoomSpawn spawn = GameObject.Find("Grid").GetComponent<RoomSpawn>();
        
        if (collision.CompareTag("Player"))
        {
            string[] coords = this.name.Split(',');

            if ( !roomEntered )
            {
                // spawns ennemies
                spawn.SpawnEnnemies(System.Convert.ToInt32(coords[0]),
                                    System.Convert.ToInt32(coords[1]));

                roomEntered = true;
            }
        }
    }
}
