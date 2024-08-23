using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;  // Speed at which the player moves
    public float jumpForce = 5f;  // Force applied for jumping

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Horizontal movement
        float moveX = Input.GetAxis("Horizontal");
        Vector2 move = new Vector2(moveX * moveSpeed, rb.velocity.y);
        rb.velocity = move;

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        // Flipping the player's direction based on movement
        if (moveX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        isGrounded = false;  // Prevent multiple jumps in the air
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
