using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    float moveSpeed = 1f;
    Rigidbody2D rigidbody2D;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        rigidbody2D.velocity = new Vector2(moveSpeed, 0f);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        FlipEnemyFacing();
        moveSpeed = -moveSpeed;
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new UnityEngine.Vector2(-rigidbody2D.velocity.x, 1f);
    }
}
