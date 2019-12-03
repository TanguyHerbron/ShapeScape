using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Generate a procedural room based on a seed and a smoothing amount.
/// </summary>
public class RoomGenerator
{
    int[,] room;
    string seed;

    /// <summary>
    /// Creates a procedural room.
    /// </summary>
    /// <param name="width">The width of the room.</param>
    /// <param name="height">The height of the room.</param>
    /// <param name="randomFillPercent">The amount of tiles randomly filled in the room before smoothing.</param>
    /// <param name="smoothing">The coefficient of how much the room is smoothen after being randomly filled.</param>
    /// <param name="seed">The seed used for the <see cref="RoomGenerator.RandomFillMap(int, int, int)"/></param>
    /// <returns>A procedurally generated room.</returns>
    public Room GenerateRoom(int width, int height, int randomFillPercent = 20, int smoothing = 2, string seed = null)
    {

        // The seed is the current timestamp.
        if(seed == null)
        {
            seed = DateTime.Now.ToString();
        }

        this.seed = seed;

        room = new int[width, height];

        RandomFillMap(width, height, randomFillPercent);

        for(int i = 0; i < smoothing; i++)
        {
            SmoothMap(width, height);
        }

        return new Room(room);
    }

    /// <summary>
    /// Fills the map using a Pseudo-Random Number Generator algorithm.
    /// </summary>
    /// <param name="width">The width of the room.</param>
    /// <param name="height">The height of the room.</param>
    /// <param name="randomFillPercent">The percentage of the room filled by the algorithm.</param>
    void RandomFillMap(int width, int height, int randomFillPercent)
    {
        System.Random prng = new System.Random(seed.GetHashCode());

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {

                // If the selected tile is a border, it's filled.
                if(x == 0 || x == width - 1 || y == 0 || y == height - 1)
                {
                    room[x, y] = 1;
                } else
                {
                    room[x, y] = (prng.Next(0, 100) < randomFillPercent) ? 1 : 0;
                }
            }
        }
    }

    /// <summary>
    /// Smooths the map after being randomly filled depending on the amount of surrouding filled tiles.
    /// </summary>
    /// <param name="width">The width of the map.</param>
    /// <param name="height">The height of the map.</param>
    void SmoothMap(int width, int height)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int neighbourWallTiles = GetSurroundingWallCount(x, y, width, height);

                if(neighbourWallTiles > 4)
                {
                    room[x, y] = 1;
                } else if(neighbourWallTiles < 4)
                {
                    room[x, y] = 0;
                }

            }
        }
    }

    /// <summary>
    /// Get the number of surrounding filled tiles.
    /// </summary>
    /// <param name="gridX">The tile x pos we want the neighbours of.</param>
    /// <param name="gridY">The tile y pos we want the neighbours of.</param>
    /// <param name="width">The width of the map.</param>
    /// <param name="height">The height of the map.</param>
    /// <returns>The number of surrounding filled tiles.</returns>
    int GetSurroundingWallCount(int gridX, int gridY, int width, int height)
    {
        int wallCount = 0;

        for(int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
        {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
            {
                // If the neihbour is not a corner
                if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < height)
                {
                    if (neighbourX != gridX || neighbourY != gridY)
                    {
                        wallCount += room[neighbourX, neighbourY];
                    }
                } else
                {
                    wallCount++;
                }
            }
        }

        return wallCount;
    }
}
