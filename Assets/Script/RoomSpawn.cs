using Assets.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomSpawn : MonoBehaviour
{
    public GameObject[] Ennemies;               // The enemies prefab to be spawned.            

    public int minEnnemies;                     // Minimum of ennemies to be spawned per room
    public int maxEnnemies;                     // Maximum of ennemies to be spawned per room

    private Tilemap groundMap;

    public void Start()
    {
        groundMap = GameObject.Find("Ground").GetComponent<Tilemap>();
    }

    /// <summary>
    /// Spawn an ennemy at a random location inside the boundaries
    /// </summary>
    public void SpawnEnnemies(int xMin, int xMax, int yMin, int yMax)
    {
        int nbOfSpawn = Random.Range(minEnnemies, maxEnnemies + 1);
        
        for(int i =0; i < nbOfSpawn; i++ )
        {
            int xPos = Random.Range(xMin, xMax);
            int yPos = Random.Range(yMin, yMax);

            if(groundMap.GetTile(new Vector3Int(xPos, yPos, 0)) != null)
            {
                Vector3 pos = new Vector3(xPos, yPos, 0);
                Instantiate(Ennemies[Random.Range(0, Ennemies.Length)], pos, new Quaternion());
            }
        }
    }
}
