using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public int[,] tiles;
    Vector2Int mapPos;

    public Room(int[,] roomTiles)
    {
        this.tiles = roomTiles;

        mapPos = Vector2Int.zero;
    }

    public void setMapPosX(int mapPosX)
    {
        this.mapPos.x = mapPosX;
    }

    public void setMapPosY(int mapPosY)
    {
        this.mapPos.y = mapPosY;
    }

    public void setMapPos(Vector2Int mapPos)
    {
        this.mapPos = mapPos;
    }

    public Vector2Int getMapPos()
    {
        return mapPos;
    }
}
