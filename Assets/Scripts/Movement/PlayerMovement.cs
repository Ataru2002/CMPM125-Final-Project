using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    const float groundSpeed = 10;
    const float detectionRange = 0.35f;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, Vector3.down * detectionRange, Color.red);
        float horizontalInput = Input.GetAxis("Horizontal");
        float horizontalVelocity = horizontalInput != 0 ? horizontalInput * groundSpeed : rb.velocity.x;

        float verticalVelocity = rb.velocity.y;
        if (Input.GetKeyDown(KeyCode.Space) && (Grounded() || TouchingWall()))
        {
            verticalVelocity = 10;
        }

        Vector2 instantVelocity = new Vector2(horizontalVelocity, verticalVelocity);
        rb.velocity = instantVelocity;
    }

    bool Grounded()
    {
        int groundMask = LayerMask.GetMask("Ground");
        return Physics2D.Raycast(transform.position, Vector2.down, detectionRange, groundMask);
    }

    bool TouchingWall()
    {
        int wallMask = LayerMask.GetMask("Wall");
        return Physics2D.Raycast(transform.position, Vector2.left, detectionRange, wallMask) || Physics2D.Raycast(transform.position, Vector2.right, 0.6f, wallMask);
    }
}
