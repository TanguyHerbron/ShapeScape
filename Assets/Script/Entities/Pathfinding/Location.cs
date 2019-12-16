using System;
using UnityEngine;

public class Location
{
    public int X;
    public int Y;
    public int F;
    public int G;
    public int H;
    public Location Parent;

    public Location(int X, int Y)
    {
        this.X = X;
        this.Y = Y;
    }

    public Location(Vector2Int vec)
    {
        this.X = vec.x;
        this.Y = vec.y;
    }
}
