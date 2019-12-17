using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public int[,] tiles;
    Vector2Int mapPos;
    private BoxCollider2D boxCollider;
    private bool ennemiesSpawned;
    private bool isCleared;

    public BoxCollider2D BoxCollider { get => boxCollider; set => boxCollider= value; }
    public bool EnnemiesSpawned { get => ennemiesSpawned; set => ennemiesSpawned= value; }
    public bool IsCleared { get => isCleared; set => isCleared= value; }

    public Room(int[,] roomTiles)
    {
        this.tiles = roomTiles;
        this.BoxCollider = new BoxCollider2D();
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
