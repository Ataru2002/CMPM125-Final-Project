using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    const float groundSpeed = 10;
    const float groundDetectionRange = 0.6f;
    const float wallDetectionRange = 0.35f;
    const float deadZone = 0.01f;
    SpriteRenderer spriteRenderer;
    Animator animator;
    Rigidbody2D rb;
    ParticleSystem runningParticles;

    Transform touchedTransform = null;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        runningParticles = transform.Find("Running Particles").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        bool grounded = Grounded();
        bool touchingWall = TouchingWall();
        bool touchingNonWall = touchedTransform != null && !grounded && !touchingWall;

        animator.SetBool("Grounded", grounded);

        float horizontalInput = Input.GetAxis("Horizontal");
        float horizontalVelocity = horizontalInput != 0 ? horizontalInput * groundSpeed : rb.velocity.x;

        // Keep the sprite flipped horizontally while it's not moving if the sprite has been flipped,
        // and make sure that it stays flipped only if the horizontal speed is non-positive (i.e., the character
        // is not moving to the right).
        spriteRenderer.flipX = (spriteRenderer.flipX || horizontalVelocity < 0) && horizontalVelocity <= 0;

        // The particles are emitted from a 45-degree cone. The source has been rotated 135 degrees so that
        // the particles trail the player while the player moves right.
        ParticleSystem.ShapeModule runningParticleShape = runningParticles.shape;
        runningParticleShape.rotation = spriteRenderer.flipX ? Vector3.zero : new Vector3(0, 0, 135);
        
        animator.SetBool("Running", grounded && horizontalInput != 0);

        if (touchingNonWall && Mathf.Sign(touchedTransform.position.x - transform.position.x) == Mathf.Sign(horizontalInput))
        {
            // Set the horizontal velocity to 0 so that the player cannot stick to walls
            // should not be clingable.
            horizontalVelocity = 0;
        }

        float verticalVelocity = rb.velocity.y;
        if (Input.GetKeyDown(KeyCode.Space) && (grounded || touchingWall))
        {
            verticalVelocity = 10;
        }

        Vector2 instantVelocity = new Vector2(horizontalVelocity, verticalVelocity);
        rb.velocity = instantVelocity;

        int verticalVelocitySign = Mathf.Abs(verticalVelocity) < deadZone ? 0 : (int)Mathf.Sign(verticalVelocity);
        animator.SetInteger("VerticalVelocitySign", verticalVelocitySign);
    }

    bool Grounded()
    {
        int groundMask = LayerMask.GetMask("Ground");
        return Physics2D.Raycast(transform.position, Vector2.down, groundDetectionRange, groundMask);
    }

    bool TouchingWall()
    {
        int wallMask = LayerMask.GetMask("Wall");
        return Physics2D.Raycast(transform.position, Vector2.left, wallDetectionRange, wallMask) || Physics2D.Raycast(transform.position, Vector2.right, 0.6f, wallMask);
    }

    public void TryEmitRunningParticles()
    {
        if (!Grounded())
        {
            return;
        }

        runningParticles.Emit(1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        touchedTransform = collision.transform;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        touchedTransform = null;
    }
}
