using Assets.Entities;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Character player;
    private Rigidbody2D rb;

    private Vector2 movement;

    public bool attacking;
    private HandManager hand;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerBehavior>().GetPlayer();
        hand = GetComponent<HandManager>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButtonDown(0) || Input.GetAxisRaw("Fire1") != 0)
        {
            hand.attack();
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * player.Speed * Time.fixedDeltaTime);
    }
}
