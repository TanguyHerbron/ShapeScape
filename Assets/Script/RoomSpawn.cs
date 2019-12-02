using Assets.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawn : MonoBehaviour
{
    public GameObject[] Ennemies;               // The enemies prefab to be spawned.            

    public int minEnnemies; // Minimum of ennemies to be spawned per room
    public int maxEnnemies; // Maximum of ennemies to be spawned per room

    /// <summary>
    /// Spawn an ennemy at a random location inside the boundaries
    /// </summary>
    public void SpawnEnnemies(int xMin, int xMax, int yMin, int yMax)
    {
        int nbOfSpawn = Random.Range(minEnnemies, maxEnnemies + 1);
        Vector2 spawnPos = new Vector2(0, 0);
        
        for(int i =0; i < nbOfSpawn; i++ )
        {
            int xPos = Random.Range(xMin, xMax);
            int yPos = Random.Range(yMin, yMax);
            Vector3 pos = new Vector3(xPos*16, yPos*16, 0);
            Instantiate(Ennemies[Random.Range(0,Ennemies.Length)], Camera.main.ScreenToWorldPoint(pos), new Quaternion());
        }
    }
}
