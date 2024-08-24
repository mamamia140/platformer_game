using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 5f;
    [SerializeField]
    float jumpSpeed = 5f;

    [SerializeField]
    float climbSpeed = 5f;

    float defaultGravity;

    Rigidbody2D rigidbody2D;

    CapsuleCollider2D collider2D;

    Animator animator;

    UnityEngine.Vector2 moveInput;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider2D = GetComponent<CapsuleCollider2D>();
        defaultGravity = rigidbody2D.gravityScale;
    }

    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<UnityEngine.Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed && collider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            rigidbody2D.velocity += new UnityEngine.Vector2(0f, jumpSpeed);
        }
    }

    void Run()
    {
        UnityEngine.Vector2 velocity = new UnityEngine.Vector2(moveInput.x * moveSpeed, rigidbody2D.velocity.y);
        rigidbody2D.velocity = velocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(rigidbody2D.velocity.x) > Mathf.Epsilon;
        animator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rigidbody2D.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new UnityEngine.Vector2(Mathf.Sign(rigidbody2D.velocity.x), 1f);

        }
    }

    void ClimbLadder()
    {

        if (collider2D.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {

            UnityEngine.Vector2 climbVelocity = new UnityEngine.Vector2(rigidbody2D.velocity.x, moveInput.y * climbSpeed);
            rigidbody2D.velocity = climbVelocity;
            rigidbody2D.gravityScale = 0;
            bool playerHasVerticalSpeed = Mathf.Abs(rigidbody2D.velocity.y) > Mathf.Epsilon;
            animator.SetBool("isClimbing", playerHasVerticalSpeed);

        }
        else
        {
            animator.SetBool("isClimbing", false);
            rigidbody2D.gravityScale = defaultGravity;
        }
    }
}
