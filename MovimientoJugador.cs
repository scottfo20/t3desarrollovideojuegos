using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MovimientoJugador : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite; 
    private Animator anim;
    [SerializeField] private LayerMask jumbleGround;
    private float dirx = 0f; 
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpForce = 18f;
    private enum MovementState { idle, Running, jumping, falling }
    [SerializeField] private AudioSource jumpSoundEffect;

    // Botones de movimiento
    [SerializeField] private Button jumpButton;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();

        jumpButton.onClick.AddListener(Jump);
    }

    private void Update()
    {
        // Asegurarse de que solo se pueda mover si el Rigidbody no está en Static
        if (rb.bodyType != RigidbodyType2D.Static)
        {
            // Movimiento con teclado
            float inputDirx = Input.GetAxisRaw("Horizontal");

            if (inputDirx != 0f)
            {
                dirx = inputDirx;
            }
            dirx = Input.GetAxisRaw("Horizontal");

            rb.velocity = new Vector2(dirx * moveSpeed, rb.velocity.y);

            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                Jump();
            }

            UpdateAnimationUpdate();
        }
        
    }

    public void WalkRight()
    {
        dirx = 1f;
    }

    public void WalkLeft()
    {
        dirx = -1f;
    }

    public void WalkStop()
    {
        dirx = 0f;
    }

    private void Jump()
    {
        if (IsGrounded() && rb.bodyType != RigidbodyType2D.Static)
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void UpdateAnimationUpdate() // Para cambiar el juadorestado a jugador corre
    {
        MovementState state;

        if (dirx > 0f)
        {
            state = MovementState.Running;
            sprite.flipX = false;
        }
        else if (dirx < 0f)
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
