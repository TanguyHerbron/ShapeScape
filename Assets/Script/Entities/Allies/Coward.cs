using Assets.Entities;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Coward : Character
{
    private GameObject player;

    public int maxDistance = 10;

    private enum State {Following, Idle, COUNT};
    private State currentState = State.Idle;

    public int speed;

    private void Start()
    {
        RigidBody = this.GetComponent<Rigidbody2D>();

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
                MoveTo(player.transform.position, Speed);
            }
            else
            {
                StopMoving();
            }
        } else
        {
            StopMoving();
        }
    }

    private void UpdateBehaviour()
    {
        currentState = (State) Random.Range(0, (float) State.COUNT);

        currentState = State.Following;
    }
}
