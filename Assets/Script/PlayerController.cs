using Assets.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player player;
    private Rigidbody2D rb;

    private Vector2 movement;

    public bool attacking;
    public Animator swordAnimator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerBehavior>().GetPlayer();
        swordAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if ( Input.GetMouseButtonDown(0) )
        {
            swordAnimator.SetTrigger("Swing");
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * player.Speed * Time.fixedDeltaTime);
    }
}
