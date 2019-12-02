using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainConstructor : MonoBehaviour
{
    public Tilemap ground;
    public Tilemap borders;

    public Tile groundTile;
    public Tile borderTile;
    public Tile redTile;

    public int maxRoomWidth = 32;
    public int maxRoomHeight = 32;

    public int minRoomWidth = 10;
    public int minRoomHeight = 10;

    public int roomCount = 5;

    public int smoothing = 2;

    [Range(0, 100)]
    public int randomFillPercentage = 20;

    List<Room> rooms;

    private const int NORTH = 1;
    private const int EAST = 2;
    private const int SOUTH = 3;
    private const int WEST = 4;

    private Room[,] roomList;

    public int corridorSize = 1;

    public GameObject player;
    List<Vector2Int> roomCenters;

    public bool drawRoomCenters = false;

    // Start is called before the first frame update
    void Start()
    {
        GenerateRooms();

        ArrangeRooms();

        DrawRooms();

        Vector2Int spawnPos = DrawCorridors();

        if(drawRoomCenters)
        {
            DrawCenters();
        }

        Instantiate(player, new Vector3(spawnPos.x, spawnPos.y), Quaternion.identity);
    }

    private void DrawCenters()
    {
        foreach(Vector2Int center in roomCenters)
        {
            ground.SetTile(new Vector3Int(center.x, center.y, 0), redTile);
        }
    }

    private void GenerateRooms()
    {
        rooms = new List<Room>();

        RoomGenerator generator = new RoomGenerator();

        for (int i = 0; i < roomCount; i++)
        {
            rooms.Add(generator.GenerateRoom(Random.Range(minRoomWidth, maxRoomWidth), Random.Range(minRoomHeight, maxRoomHeight), randomFillPercentage, smoothing));
        }
    }

    private void ArrangeRooms()
    {
        const int maxRoomTries = 100;

        roomList = new Room[1 + rooms.Count / (rooms.Count / 2), 1 + rooms.Count / (rooms.Count / 2)];

        for (int i = 0; i < rooms.Count; i++)
        {
            int roomPlacementTries = 0;

            int values = roomList.GetLength(0) * roomList.GetLength(1);
            int index = Random.Range(0, values);

            while(roomPlacementTries < maxRoomTries)
            {
                roomPlacementTries++;

                if (roomList[index / roomList.GetLength(0), index % roomList.GetLength(1)] == null)
                {
                    roomList[index / roomList.GetLength(0), index % roomList.GetLength(1)] = rooms[i];
                    roomPlacementTries = maxRoomTries;
                }
            }
        }
    }

    private void DrawRooms()
    {
        for(int x = 0; x < roomList.GetLength(0); x++)
        {
            for(int y = 0; y < roomList.GetLength(1); y++)
            {
                for (int xTile = 0; xTile < maxRoomWidth; xTile++)
                {
                    for (int yTile = 0; yTile < maxRoomHeight; yTile++)
                    {
                        if (roomList[x, y] != null && roomList[x, y].tiles.GetLength(0) > xTile && roomList[x, y].tiles.GetLength(1) > yTile && roomList[x, y].tiles[xTile, yTile] == 0)
                        {
                            ground.SetTile(new Vector3Int(xTile + x * maxRoomWidth, yTile + y * maxRoomHeight, 0), groundTile);
                        }
                        else
                        {
                            borders.SetTile(new Vector3Int(xTile + x * maxRoomWidth, yTile + y * maxRoomHeight, 0), borderTile);
                        }
                    }
                }
            }
        }
    }

    private Vector2Int DrawCorridors()
    {
        List<Vector2Int> corridorEndpoints = new List<Vector2Int>();

        for(int x = 0; x < roomList.GetLength(0); x++)
        {
            for(int y = 0; y < roomList.GetLength(1); y++)
            {
                if(roomList[x, y] != null)
                {
                    corridorEndpoints.Add(new Vector2Int(x * maxRoomWidth + roomList[x, y].tiles.GetLength(0) / 2, y * maxRoomHeight + roomList[x, y].tiles.GetLength(1) / 2));
                }
            }
        }

        roomCenters = new List<Vector2Int>(corridorEndpoints);

        while(corridorEndpoints.Count > 1)
        {
            Vector2Int start = corridorEndpoints[Random.Range(0, corridorEndpoints.Count)];
            corridorEndpoints.Remove(start);

            Vector2Int destination = corridorEndpoints[Random.Range(0, corridorEndpoints.Count)];

            while (start != destination)
            {
                if (start.x > destination.x)
                {
                    start.x--; ;
                }
                else if (start.x < destination.x)
                {
                    start.x++; ;
                }

                if (start.y > destination.y)
                {
                    start.y--; ;
                }
                else if (start.y < destination.y)
                {
                    start.y++;
                }

                int offset = Mathf.RoundToInt(10 * Mathf.PerlinNoise(start.x / 8f, start.y / 8f) / 2);

                for (int x = start.x - corridorSize; x < start.x + corridorSize; x++)
                {
                    for (int y = start.y - corridorSize; y < start.y + corridorSize; y++)
                    {
                        ground.SetTile(new Vector3Int(x + offset, y - offset / 2, 0), groundTile);
                        borders.SetTile(new Vector3Int(x + offset, y - offset / 2, 0), null);
                    }
                }
            }
        }

        return corridorEndpoints[0];
    }
}
