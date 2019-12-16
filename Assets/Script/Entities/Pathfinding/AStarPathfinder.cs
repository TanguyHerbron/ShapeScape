using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AStarPathfinder
{
    private Location current;
    private Location start;
    private Location target;
    private List<Location> openList;
    private List<Location> closedList;
    private int g;

    private Tilemap groundTilemap;
    private Tilemap corridorTilemap;

    public AStarPathfinder(Location start, Location target, Tilemap groundTilemap, Tilemap corridorTilemap)
    {
        this.current = null;
        this.start = start;
        this.target = target;
        openList = new List<Location>();
        closedList = new List<Location>();
        g = 0;

        this.groundTilemap = groundTilemap;
        this.corridorTilemap = corridorTilemap;

        openList.Add(start);
    }

    public void ComputePath()
    {
        while(openList.Count > 0)
        {
            int lowest = GetLowestF();
            current = GetFirstLowestFLocation(lowest);

            closedList.Add(current);
            openList.Remove(current);

            if (GetFirstOrDefault(closedList, target.X, target.Y) != null)
            {
                break;
            }

            List<Location> adjacentSquares = GetWalkableAdjacentSquares(current.X, current.Y);

            foreach(Location adjacentSquare in adjacentSquares)
            {
                if (GetFirstOrDefault(closedList, adjacentSquare.X, adjacentSquare.Y) != null)
                    continue;

                if (GetFirstOrDefault(openList, adjacentSquare.X, adjacentSquare.Y) == null)
                {
                    adjacentSquare.G = g;
                    adjacentSquare.H = ComputeHScore(adjacentSquare.X, adjacentSquare.Y, target.X, target.Y);
                    adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                    adjacentSquare.Parent = current;

                    openList.Insert(0, adjacentSquare);
                } else
                {
                    if(g + adjacentSquare.H < adjacentSquare.F)
                    {
                        adjacentSquare.G = g;
                        adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                        adjacentSquare.Parent = current;
                    }
                }
            }
        }
    }

    public List<Location> GetPath()
    {
        List<Location> path = new List<Location>();

        while(current.Parent != null)
        {
            path.Insert(0, current);

            current = current.Parent;
        }

        return path;
    }

    private List<Location> GetWalkableAdjacentSquares(int x, int y)
    {
        List<Location> proposedLocations = new List<Location>()
        {
            new Location(x, y - 1),
            new Location(x, y + 1),
            new Location(x - 1, y),
            new Location(x + 1, y)
        };

        int index = 0;

        while(index < proposedLocations.Count)
        {
            if (!groundTilemap.HasTile(new Vector3Int(proposedLocations[index].X, proposedLocations[index].Y, 0))
                && !corridorTilemap.HasTile(new Vector3Int(proposedLocations[index].X, proposedLocations[index].Y, 0)))
            {
                proposedLocations.Remove(proposedLocations[index]);
            } else
            {
                index++;
            }
        }

        return proposedLocations;
    }

    private Location GetFirstOrDefault(List<Location> list, int targetX, int targetY)
    {
        Location ret = null;

        bool found = false;
        int index = 0;

        while(index < list.Count && !found)
        {
            if(list[index].X == targetX && list[index].Y == targetY)
            {
                ret = list[index];

                found = true;
            }

            index++;
        }

        return ret;
    }

    private Location GetFirstLowestFLocation(int lowestF)
    {
        Location first = openList[0];

        bool found = false;
        int index = 0;

        while(first.F != lowestF && !found) {

            if(openList[index].F == lowestF)
            {
                first = openList[index];
                found = true;
            }

            index++;
        }

        return first;
    }

    private int GetLowestF()
    {
        int min = openList[0].F;

        for(int i = 1; i < openList.Count; i++)
        {
            if(min > openList[i].F)
            {
                min = openList[i].F;
            }
        }

        return min;
    }

    static int ComputeHScore(int x, int y, int targetX, int targetY)
    {
        return Mathf.Abs(targetX - x) + Mathf.Abs(targetY - y);
    }
}
