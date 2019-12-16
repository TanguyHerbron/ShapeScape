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

    public bool straightCorridor = true;
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

        DrawWalls();

        if(drawRoomCenters)
        {
            DrawCenters();
        }

        SpawnExit(spawnPos);

        Instantiate(player, new Vector3(spawnPos.x, spawnPos.y), Quaternion.identity);
        Instantiate(ally, new Vector3(spawnPos.x + Random.Range(1, 3), spawnPos.y + Random.Range(1, 3)), Quaternion.identity);
    }

    /// <summary>
    /// Draws a wall layer around rooms and corridors.
    /// </summary>
    private void DrawWalls()
    {
        for(int x = 0; x < ground.size.x; x++)
        {
            for(int y = 0; y < ground.size.y; y++)
            {
                if(ground.GetTile(new Vector3Int(x, y, 0)) == null && corridor.GetTile(new Vector3Int(x, y, 0)) == null && HasNeighbouringGround(x, y))
                {
                    borders.SetTile(new Vector3Int(x, y, 0), borderTile);
                }
            }
        }
    }

    /// <summary>
    /// Return true if there is a ground tile around the position given in parameter.
    /// </summary>
    /// <param name="tileX">Center on the X axis</param>
    /// <param name="tileY">Center on the Y Axis</param>
    /// <returns>True if there is a neighbouring ground tile</returns>
    private bool HasNeighbouringGround(int tileX, int tileY)
    {
        bool hasNeighbour = false;

        int x = tileX - 1;

        while(x <= tileX + 1 && !hasNeighbour)
        {
            int y = tileY - 1;

            while (y <= tileY + 1 && !hasNeighbour)
            {
                if(x != tileX && y != tileY)
                {
                    hasNeighbour = ground.GetTile(new Vector3Int(x, y, 0)) == groundTile || corridor.GetTile(new Vector3Int(x, y, 0)) == corridorTile;
                }

                y++;
            }

            x++;
        }

        return hasNeighbour;
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
    /// Creates a game object with a collider around the room for entering and exiting detection.
    /// We also add the RoomManager component for the detection
    /// </summary>
    /// <param name="x">The x center of the room.</param>
    /// <param name="y">The y center of the room.</param>
    /// <param name="room">The room for which we create a collider.</param>
    private void SpawnRoomGameObject(int x, int y, Room room)
    {
        GameObject roomGameObject = new GameObject();

        roomGameObject.name = (x * maxRoomWidth) + "," + (y * maxRoomHeight) + "," + (x * maxRoomWidth + maxRoomWidth) + "," + (y * maxRoomHeight + maxRoomHeight);
        roomGameObject.AddComponent<RoomManager>();

        BoxCollider2D collider = roomGameObject.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(room.tiles.GetLength(0), room.tiles.GetLength(1));
        collider.isTrigger = true;

        roomGameObject.transform.position = new Vector3(x * maxRoomWidth + room.tiles.GetLength(0) / 2, y * maxRoomHeight + room.tiles.GetLength(1) / 2);
        roomGameObject.transform.parent = transform;
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
                    }
                }

                // If there is a room at this index, we generate a collider for this room.
                if(roomList[x, y] != null)
                {
                    SpawnRoomGameObject(x, y, roomList[x, y]);
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

        // If straightCorridor is set to true, will draw straight corridor, otherwise will try to draw organic looking corridors.

        // For every roomCenters, we try to find a path towards another random selected roomCenter.
        // If a path is found, we remove the start point from the roomCenter list to not have multiple paths towards one room.
        if (straightCorridor)
        {
            while (corridorEndpoints.Count > 1)
            {
                Vector2Int start = corridorEndpoints[Random.Range(0, corridorEndpoints.Count)];
                corridorEndpoints.Remove(start);

                Vector2Int destination = corridorEndpoints[Random.Range(0, corridorEndpoints.Count)];

                while (start.x != destination.x)
                {
                    start.x += Mathf.FloorToInt(Mathf.Sign(destination.x - start.x));

                    DrawCorridorTile(start);
                }

                while(start.y != destination.y)
                {
                    start.y += Mathf.FloorToInt(Mathf.Sign(destination.y - start.y));

                    DrawCorridorTile(start);
                }
            }
        } else
        {
            while (corridorEndpoints.Count > 1)
            {
                Vector2Int start = corridorEndpoints[Random.Range(0, corridorEndpoints.Count)];
                corridorEndpoints.Remove(start);

                Vector2Int destination = corridorEndpoints[Random.Range(0, corridorEndpoints.Count)];

                while (start != destination)
                {
                    if (start.x > destination.x)
                    {
                        start.x--;
                    }
                    else if (start.x < destination.x)
                    {
                        start.x++;
                    }

                    if (start.y > destination.y)
                    {
                        start.y--;
                    }
                    else if (start.y < destination.y)
                    {
                        start.y++;
                    }

                    int offset = Mathf.RoundToInt(10 * Mathf.PerlinNoise(start.x / 8f, start.y / 8f) / 2);

                    DrawCorridorTile(start, offset);
                }
            }
        }

        return corridorEndpoints[0];
    }

    /// <summary>
    /// Places a corridor tile on the corridor tilemap and removes any wall at the same position from the border tilemap
    /// </summary>
    /// <param name="pos">Position to draw around</param>
    /// <param name="offset">Optional offset, mainly used to noise</param>
    void DrawCorridorTile(Vector2Int pos, int offset = 0)
    {
        for (int x = pos.x; x < pos.x + corridorSize; x++)
        {
            for (int y = pos.y; y < pos.y + corridorSize; y++)
            {
                corridor.SetTile(new Vector3Int(x + offset, y - offset / 2, 0), corridorTile);
                borders.SetTile(new Vector3Int(x + offset, y - offset / 2, 0), null);
            }
        }
    }
}
