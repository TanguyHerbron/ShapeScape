using Assets.Entities;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Coward : Character
{
    private GameObject player;
    private Vector3 oldTarget;
    private Location oldPathTarget;

    private List<Location> path;

    public int maxDistance = 10;

    private enum State {Following, Idle, COUNT};
    private State currentState = State.Idle;

    private Rigidbody2D rb;
    public int speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Name = "Michel";
        HP = 6;
        Speed = speed;

        InvokeRepeating("UpdateBehaviour", 0f, 2f);
    }

    private void FixedUpdate()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if(Vector3.Distance(player.transform.position, transform.position) > maxDistance)
        {
            if(currentState == State.Following)
            {
                Location pathTarget = new Location(Mathf.FloorToInt(player.transform.position.x)
                    , Mathf.FloorToInt(player.transform.position.y));

                if (pathTarget != oldPathTarget)
                {
                    Location start = new Location(Mathf.FloorToInt(transform.position.x)
                    , Mathf.FloorToInt(transform.position.y));

                    Tilemap groundTilemap = GameObject.FindGameObjectsWithTag("Walkable")[0].GetComponent<Tilemap>();
                    Tilemap corridorTilemap = GameObject.FindGameObjectsWithTag("Walkable")[1].GetComponent<Tilemap>();

                    AStarPathfinder aStar = new AStarPathfinder(start, pathTarget, groundTilemap, corridorTilemap);

                    aStar.ComputePath();

                    path = aStar.GetPath();
                }

                if(path != null && path.Count > 0)
                {
                    Vector2 target = (new Vector3(path[0].X + 0.5f, path[0].Y + 0.5f, 0) - transform.position).normalized;

                    rb.velocity = Vector2.zero;
                    rb.AddForce(target * speed);
                }
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        } else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void UpdateBehaviour()
    {
        currentState = (State) Random.Range(0, (float) State.COUNT);

        currentState = State.Following;
    }
}
