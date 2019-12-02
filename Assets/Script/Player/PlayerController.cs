﻿using Assets.Entities;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Character player;
    private Rigidbody2D rb;

    private Vector2 mouseOnScreen;
    private Vector2 positionOnScreen;
    private float angle;

    private Vector2 movement;

    public bool attacking;
    private HandManager hand;

    private Vector2 oldMovement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerBehavior>().GetPlayer();
        hand = GetComponent<HandManager>();

        positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);
        mouseOnScreen = (Vector2) Camera.main.ScreenToViewportPoint(Input.mousePosition);
    }

    void Update()
    {
        // Moving the character
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Orienting the character
        // Get the screen positions of the player then get the screen position of the mouse
        // Then calculating the angle between the two, and applying the angle to the player's rotation
        positionOnScreen = Camera.main.WorldToViewportPoint(rb.position);
        mouseOnScreen = (Vector2) Camera.main.ScreenToViewportPoint(Input.mousePosition);
        angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
        rb.rotation =  angle;

        // Checking for attacks
        if ( Input.GetMouseButtonDown(0) || Input.GetAxisRaw("Fire1") != 0 )
        {
            hand.attack();
        }
    }

    private void FixedUpdate()
    {
        if(movement != oldMovement)
        {
            rb.velocity = Vector2.zero;
            oldMovement = movement;
        }

        if (rb.velocity == Vector2.zero)
        {
            rb.AddForce(movement * player.Speed);
        }

        //rb.MovePosition(rb.position + movement * player.Speed * Time.fixedDeltaTime);
    }

    /// <summary>
    /// Get the angle between two vector3
    /// </summary>
    /// <param name="a">First Vector</param>
    /// <param name="b">Second Vector</param>
    /// <returns></returns>
    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
