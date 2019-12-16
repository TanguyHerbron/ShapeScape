using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainConstructor : MonoBehaviour
{
    public Tilemap ground;
    public Tilemap borders;
    public Tilemap corridor;

    public Tile groundTile;
    public Tile borderTile;
    public Tile corridorTile;
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
    public GameObject ally;
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

        SpawnExit(spawnPos);

        Instantiate(player, new Vector3(spawnPos.x, spawnPos.y), Quaternion.identity);
        Instantiate(ally, new Vector3(spawnPos.x + Random.Range(1, 3), spawnPos.y + Random.Range(1, 3)), Quaternion.identity);
    }

    /// <summary>
    /// Creates an exit point for the level.
    /// This point is represented by <c>redTile</c>.
    /// </summary>
    /// <param name="spawnPos">The coordinates of the exit point.</param>
    private void SpawnExit(Vector2Int spawnPos)
    {
        Vector2Int exit = Vector2Int.zero;

        do
        {
            exit = roomCenters[Random.Range(0, roomCenters.Count)];
        } while (exit == spawnPos);

        ground.SetTile(new Vector3Int(exit.x, exit.y, 0), redTile);

        GameObject end = new GameObject();
        end.name = "End";
        BoxCollider2D collider = end.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(1, 1);
        collider.offset = new Vector2(0.5f, 0.5f);
        collider.isTrigger = true;

        end.transform.position = new Vector3(exit.x, exit.y, 0);
        end.transform.parent = transform;
    }

    /// <summary>
    /// Creates a collider around the room for entering and exiting detection.
    /// </summary>
    /// <param name="x">The x center of the room.</param>
    /// <param name="y">The y center of the room.</param>
    /// <param name="room">The room for which we create a collider.</param>
    private void SpawnRoomCollider(int x, int y, Room room)
    {
        GameObject colliderObject = new GameObject();
        colliderObject.name = (x * maxRoomWidth) + "," + (y * maxRoomHeight) + "," + (x * maxRoomWidth + maxRoomWidth) + "," + (y * maxRoomHeight + maxRoomHeight) + "," + " ";
        BoxCollider2D collider = colliderObject.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(room.tiles.GetLength(0), room.tiles.GetLength(1));
        collider.isTrigger = true;
        colliderObject.transform.position = new Vector3(x * maxRoomWidth + room.tiles.GetLength(0) / 2, y * maxRoomHeight + room.tiles.GetLength(1) / 2);
        colliderObject.transform.parent = transform;
    }

    /// <summary>
    /// Draw the center of every room, for debug purposes.
    /// </summary>
    private void DrawCenters()
    {
        foreach(Vector2Int center in roomCenters)
        {
            ground.SetTile(new Vector3Int(center.x, center.y, 0), redTile);
        }
    }


    /// <summary>
    /// <para>Creates a list of <paramref name="roomCount"/> rooms.</para>
    /// <para>Their heights is randomly chosen between <paramref name="minRoomHeight"/> and <paramref name="maxRoomHeight"/>.</para>
    /// <para>Their widths is randomly chosen between <paramref name="minRoomWidth"/> and <paramref name="maxRoomWidth"/>.</para>
    /// </summary>
    private void GenerateRooms()
    {
        rooms = new List<Room>();

        RoomGenerator generator = new RoomGenerator();

        for (int i = 0; i < roomCount; i++)
        {
            rooms.Add(generator.GenerateRoom(Random.Range(minRoomWidth, maxRoomWidth), Random.Range(minRoomHeight, maxRoomHeight), randomFillPercentage, smoothing));
        }
    }

    /// <summary>
    /// Randomly places the rooms across the map.
    /// </summary>
    private void ArrangeRooms()
    {
        const int maxRoomTries = 100;

        roomList = new Room[1 + rooms.Count / (rooms.Count / 2), 1 + rooms.Count / (rooms.Count / 2)];

        // For each room.
        for (int i = 0; i < rooms.Count; i++)
        {
            int roomPlacementTries = 0;

            int values = roomList.GetLength(0) * roomList.GetLength(1);
            int index = Random.Range(0, values);

            // Try to place the selected room, if the randomly found index is not empty, another index is generated.
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

    /// <summary>
    /// Draws every rooms on the tilemap.
    /// If a tile is not part of a room, it's filled with a wall.
    /// </summary>
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

                // If there is a room at this index, we generate a collider for this room.
                if(roomList[x, y] != null)
                {
                    SpawnRoomCollider(x, y, roomList[x, y]);
                }
            }
        }
    }

    /// <summary>
    /// Draws avery corridos on the tilemap.
    /// </summary>
    /// <returns>The spawn point for the player and ally.</returns>
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

        // For every roomCenters, we try to find a patch towards another random selected roomCenter.
        // If a path is found, we remove the start point from the roomCenter list to not have multiple paths towards one room.
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
                        corridor.SetTile(new Vector3Int(x + offset, y - offset / 2, 0), corridorTile);
                        borders.SetTile(new Vector3Int(x + offset, y - offset / 2, 0), null);
                    }
                }
            }
        }

        return corridorEndpoints[0];
    }
}
