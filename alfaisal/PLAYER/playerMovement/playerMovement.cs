using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public float gravityScale = 1f; // Field to adjust fall speed

    private Rigidbody2D rb;
    private bool isGrounded;
    public LayerMask groundLayer;
    public Transform feetPos;
    public float checkRadius;
    public float jumpTime;
    private float jumpTimeCounter;

    public float coyoteTime = 0.2f; // Duration of coyote time
    private float coyoteTimeCounter; // Timer for coyote time

    public SpriteRenderer spriteRenderer; // Reference to SpriteRenderer

    private bool isGrabbing;
    public float grabDuration = 2f; // Duration for which the player can hold onto an object
    private float grabTimeCounter;
    public LayerMask grabbableLayer;
    public Transform grabCheck;
    public float grabCheckRadius;
    private GameObject grabbableObject;

    public KeyCode grabKey = KeyCode.E; // Key to grab
    public KeyCode climbKey = KeyCode.Space; // Key to climb

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Initialize SpriteRenderer
        rb.gravityScale = gravityScale; // Set initial gravity scale
    }

    private void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");

        // Move the player
        if (!isGrabbing)
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

            // Flip the sprite based on movement direction
            if (moveInput > 0)
            {
                spriteRenderer.flipX = false; // Facing right
            }
            else if (moveInput < 0)
            {
                spriteRenderer.flipX = true; // Facing left
            }
        }

        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, groundLayer);

        // Update coyote time counter
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        // Handle jumping
        if ((isGrounded || coyoteTimeCounter > 0) && Input.GetButtonDown("Jump"))
        {
            rb.velocity = Vector2.up * jumpForce;
            jumpTimeCounter = jumpTime;
        }

        if (Input.GetButton("Jump"))
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            jumpTimeCounter = 0;
        }

        // Handle grabbing
        if (Input.GetKeyDown(grabKey))
        {
            if (isGrabbing)
            {
                ReleaseGrab();
            }
            else
            {
                TryGrab();
            }
        }

        if (isGrabbing)
        {
            grabTimeCounter -= Time.deltaTime;
            if (grabTimeCounter <= 0)
            {
                ReleaseGrab();
            }

            // Allow player to climb while grabbing
            if (Input.GetKeyDown(climbKey))
            {
                Climb();
            }
        }

        // Update grabCheck position based on the direction the player is facing
        if (spriteRenderer.flipX) // Facing left
        {
            grabCheck.localPosition = new Vector2(-Mathf.Abs(grabCheck.localPosition.x), grabCheck.localPosition.y);
        }
        else // Facing right
        {
            grabCheck.localPosition = new Vector2(Mathf.Abs(grabCheck.localPosition.x), grabCheck.localPosition.y);
        }
    }

    private void FixedUpdate()
    {
        // Apply custom gravity scale to control fall speed
        rb.gravityScale = gravityScale;
    }

    private void TryGrab()
    {
        Collider2D grabbable = Physics2D.OverlapCircle(grabCheck.position, grabCheckRadius, grabbableLayer);
        if (grabbable != null)
        {
            isGrabbing = true;
            grabTimeCounter = grabDuration;
            grabbableObject = grabbable.gameObject;
            rb.velocity = Vector2.zero; // Stop player movement
            rb.gravityScale = 0f; // Disable gravity while grabbing
            rb.isKinematic = true; // Make player kinematic while grabbing
        }
    }

    private void ReleaseGrab()
    {
        isGrabbing = false;
        rb.gravityScale = gravityScale; // Restore gravity
        rb.isKinematic = false; // Make player non-kinematic
        grabbableObject = null;
    }

    private void Climb()
    {
        // Implement climbing logic here
        // Example: Move player upwards by a certain amount
        rb.velocity = new Vector2(rb.velocity.x, moveSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(feetPos.position, checkRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(grabCheck.position, grabCheckRadius);
    }
}
