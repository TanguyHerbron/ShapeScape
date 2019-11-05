using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Characters.Ennemies;

public class BeserkerBehaviour: MonoBehaviour
{
    private Ennemy ennemy;

    private Vector3 ennemyPosition;
    private Vector3 playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        ennemy = new Ennemy("Beserker", 5);

        // Init of the rotation and position of a ennemy
        playerPosition = GameObject.Find("Player").transform.position;
        MoveTo(playerPosition, ennemy.Speed);
    }

    // Update is called once per frame
    void Update()
    {
        // Stops the ennemies depending on their state
        if (ennemy.IsDead())
        {
            StopMoving();
        }
        else
        {
            // Update of the rotation and position of a ennemy
            playerPosition = GameObject.Find("Player").transform.position;
            MoveTo(playerPosition, ennemy.Speed);
        }
    }

    /// <summary>
    /// Accessor for the ennemy
    /// </summary>
    /// <returns>the current bear</returns>
    public Ennemy GetEnnemy()
    {
        return ennemy;
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
}
