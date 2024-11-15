using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite; 
    private Animator anim;
    [SerializeField] private LayerMask jumbleGround;
    private float dirx = 0f; 
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpForce = 10f;
    private enum MovementState { idle, Running, jumping, falling }
    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        sprite =GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        dirx = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirx * moveSpeed,rb.velocity.y);
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x,jumpForce);
        }
        UpdateAnimationUpdate();
    }
    private void UpdateAnimationUpdate() //Para cambiar el juadorestado a jugadorcorre
    {
        MovementState state;
        if(dirx > 0f)
        {
            state = MovementState.Running;
            sprite.flipX = false;
        }
        else if(dirx < 0f)
        {
            state = MovementState.Running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }
        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumbleGround);
    }
}
