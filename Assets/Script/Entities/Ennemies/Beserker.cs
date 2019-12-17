using Assets.Entities;
using System.Collections;
using UnityEngine;

public class Beserker: Ennemy
{
    private GameObject player;

    public float beserkerSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Name = "Beserker";
        HP = 5;
        Speed = beserkerSpeed;

        RigidBody = GetComponent<Rigidbody2D>();

        GetComponent<ParticleSystem>().Stop();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ( RigidBody == null )
        {
            RigidBody = this.GetComponent<Rigidbody2D>();
        }

        if ( player == null )
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

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
            player = GameObject.FindGameObjectWithTag("Player");
            MoveTo(player.transform.position, Speed);
        }
    }

    /// <summary>
    /// Temporary : Kills the berserker
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<ParticleSystem>().Play();
            StartCoroutine(WaitBeforeDestroy());
        }
    }

    /// <summary>
    /// Waits for the particicle effect before detroying gameobject
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitBeforeDestroy()
    {
        yield return new WaitForSeconds(1);
        Kill();
        GameObject.Destroy(this.gameObject);
    }
}
