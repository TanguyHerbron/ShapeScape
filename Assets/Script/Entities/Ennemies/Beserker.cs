using Assets.Entities;
using UnityEngine;

public class Beserker: Ennemy
{
    private Vector3 ennemyPosition;
    private Vector3 playerPosition;

    public float beserkerSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Name = "Beserker";
        HP = 5;
        Speed = beserkerSpeed;


        GetComponent<ParticleSystem>().Stop();

        // Init of the rotation and position of a ennemy
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        MoveTo(playerPosition, Speed);
    }

    // Update is called once per frame
    void Update()
    {
        // Stops the ennemies depending on their state
        if (IsDead())
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
            MoveTo(playerPosition, Speed);
        }
    }

    /// <summary>
    /// Moves the character towards the target
    /// Also orienting the character towards the targer
    /// </summary>
    /// <param name="target">The target Vector3 that the entity should move towards</param>
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Melee Weapon"))
        {
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<ParticleSystem>().Play();
            Kill();
        }
    }
}
