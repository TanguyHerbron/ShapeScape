using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Entities;

public class BeserkerBehaviour: MonoBehaviour
{
    private Character beserker;

    private Vector3 ennemyPosition;
    private Vector3 playerPosition;

    public float beserkerSpeed;

    private ParticleSystem particleSystemm;

    // Start is called before the first frame update
    void Start()
    {
        beserker = new Character("Beserker", 5, beserkerSpeed);

        // Init of the rotation and position of a ennemy
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        MoveTo(playerPosition, beserker.Speed);
    }

    // Update is called once per frame
    void Update()
    {
        // Stops the ennemies depending on their state
        if (beserker.IsDead())
        {
            StopMoving();
            if(!GetComponent<ParticleSystem>().IsAlive())
            {
                Destroy(this);
            }
        }
        else
        {
            // Update of the rotation and position of a ennemy
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            MoveTo(playerPosition, beserker.Speed);
        }
    }

    /// <summary>
    /// Accessor for the ennemy
    /// </summary>
    /// <returns>the current bear</returns>
    public Character GetEnnemy()
    {
        return beserker;
    }

    /// <summary>
    /// Moves the character towards the target
    /// Also orienting the character towards the targer
    /// </summary>
    /// <param name="target"></param>
    public void MoveTo(Vector3 target, float movingSpeed)
    {
        ennemyPosition = Vector2.MoveTowards(transform.position, target, Time.deltaTime * movingSpeed);
        Vector3 DirectionOfRotation = target - transform.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, DirectionOfRotation);

        transform.position = ennemyPosition;
    }

    /// <summary>
    /// Stops the character movements
    /// </summary>
    public void StopMoving()
    {
        transform.position = transform.position;
        transform.rotation = transform.rotation;
    }

    /// <summary>
    /// Temporary : Kills the berserker
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<ParticleSystem>().Play();
            beserker.Kill();
        }
    }
}
