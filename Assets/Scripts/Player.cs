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

    [SerializeField]
    float deathKick = 8f;

    [SerializeField]
    GameObject arrow;

    [SerializeField]
    GameObject weapon;


    float defaultGravity;

    public bool isAlive = true;

    Rigidbody2D rigidbody2D;

    CapsuleCollider2D capsuleCollider2D;

    BoxCollider2D boxCollider2D;

    Animator animator;

    UnityEngine.Vector2 moveInput;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        defaultGravity = rigidbody2D.gravityScale;
    }

    void Update()
    {
        if (!isAlive)
        {
            return;
        }
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<UnityEngine.Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }
        if (value.isPressed && boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            rigidbody2D.velocity += new UnityEngine.Vector2(0f, jumpSpeed);
        }
    }

    void OnFire(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }

        if (value.isPressed)
        {
            GameObject.Instantiate(arrow, weapon.transform.position, UnityEngine.Quaternion.identity);
        }
    }

    void Run()
    {
        if (!isAlive)
        {
            return;
        }
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

        if (!isAlive)
        {
            return;
        }
        if (capsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing")))
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

    void Die()
    {
        if (capsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemies")) || boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemies")) ||
         capsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Hazard")) || boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Hazard")))
        {
            isAlive = false;
            animator.SetTrigger("Dying");
            rigidbody2D.velocity = new UnityEngine.Vector2(rigidbody2D.velocity.x * moveSpeed, deathKick);
        }

    }
}
