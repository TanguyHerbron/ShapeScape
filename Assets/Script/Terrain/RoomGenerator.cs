using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoomGenerator
{
    int[,] room;
    string seed;

    public Room GenerateRoom(int width, int height, int randomFillPercent = 20, int smoothing = 2, string seed = null)
    {
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

    void RandomFillMap(int width, int height, int randomFillPercent)
    {
        System.Random prng = new System.Random(seed.GetHashCode());

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
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

    int GetSurroundingWallCount(int gridX, int gridY, int width, int height)
    {
        int wallCount = 0;

        for(int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
        {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
            {
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
