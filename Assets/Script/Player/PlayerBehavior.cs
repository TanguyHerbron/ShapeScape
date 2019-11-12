using Assets.Entities;
using System.Collections;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private Character player;

    // Start is called before the first frame update
    void Start()
    {
        player = new Character("Test", 5, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    /// <summary>
    /// Collision detector
    /// Checks if the object colliding with the player should or not apply damage
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( (collision.gameObject.CompareTag("Melee Weapon") || collision.gameObject.CompareTag("Ennemy")) && !player.Invicible)
        {
            player.ApplyDamage(1);
            StartCoroutine(Invicibility());
        }
    }

    /// <summary>
    /// Provides immunity to the player for one second
    /// By setting the immune boolean to true
    /// </summary>
    /// <returns></returns>
    private IEnumerator Invicibility()
    {
        player.Invicible = true;
        yield return new WaitForSeconds(1);
        player.Invicible = false;
    }

    public Character GetPlayer()
    {
        if(player == null)
        {
            player = new Character("Test", 5, 5);
        }

        return player;
    }
}
