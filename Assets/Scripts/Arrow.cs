using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    float speed = 5f;

    float xspeed;
    Rigidbody2D rigidbody2D;

    CapsuleCollider2D capsuleCollider2D;
    Player player;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        player = FindObjectOfType<Player>();
        xspeed = player.transform.localScale.x * speed;
    }

    void Update()
    {
        rigidbody2D.velocity = new Vector2(xspeed, 0f); 
        FlipSprite();   
    }

    void FlipSprite()
    {
        bool hasHorizontalSpeed = Mathf.Abs(rigidbody2D.velocity.x) > Mathf.Epsilon;

        if (hasHorizontalSpeed)
        {
            transform.localScale = new UnityEngine.Vector2(Mathf.Sign(rigidbody2D.velocity.x), 1f);

        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(capsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemies"))){
            Destroy(other.gameObject);
        }

        Destroy(this.gameObject);
    }   

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.otherCollider.IsTouchingLayers(LayerMask.GetMask("Enemies"))){
            Destroy(collision.gameObject);
        }
        Destroy(this.gameObject);
    }
}
