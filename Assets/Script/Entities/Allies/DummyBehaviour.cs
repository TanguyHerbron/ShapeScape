using Assets.Entities;
using System.Collections;
using UnityEngine;

public class DummyBehaviour : Ennemy
{
    private GameObject player;

    public int DummySpeed;

    void Start()
    {
        Name = "Dummy";
        HP = 5;
        Speed = DummySpeed;

        RigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if ( player == null )
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if ( RigidBody == null )
        {
            RigidBody = this.GetComponent<Rigidbody2D>();
        }

        MoveTo(player.transform.position, Speed);
    }

    /// <summary>
    /// Temporary : Kills the berserker
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.gameObject.CompareTag("Weapon") )
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
