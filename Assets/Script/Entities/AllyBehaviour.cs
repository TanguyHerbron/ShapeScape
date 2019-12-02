using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Entities;

public class AllyBehaviour : MonoBehaviour
{
    private Character character;
    private GameObject player;
    private Vector3 oldTarget;

    public int maxDistance = 10;

    private enum State {Following, Idle, COUNT};
    private State currentState = State.Idle;

    private Rigidbody2D rb;
    public int speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        character = new Character("Michel", 100, speed);

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
                Vector3 target = (player.transform.position - transform.position).normalized;

                if(target != oldTarget)
                {
                    oldTarget = target;
                    rb.velocity = Vector2.zero;
                }

                if (rb.velocity == Vector2.zero)
                {
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
    }
}
