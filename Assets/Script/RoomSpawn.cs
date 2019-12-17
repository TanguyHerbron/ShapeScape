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

    private TerrainConstructor terrainConstructor;

    public void Start()
    {
        terrainConstructor = TerrainConstructor.instance;
    }

    /// <summary>
    /// Spawn an ennemy at a random location inside the boundaries
    /// </summary>
    public void SpawnEnnemies(int x, int y)
    {
        int nbOfSpawn = Random.Range(minEnnemies, maxEnnemies + 1);
        
        for(int i =0; i < nbOfSpawn; i++ )
        {
            Vector2 spawn = terrainConstructor.GetRandomSpawnableTile(new Vector2Int(x, y));

            Instantiate(Ennemies[Random.Range(0, Ennemies.Length)], new Vector3(spawn.x, spawn.y), new Quaternion());
        }
    }
}
