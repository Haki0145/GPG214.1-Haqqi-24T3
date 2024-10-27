using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // Player movement speed
    public float jumpForce = 7f;  // Force applied when jumping
    public Transform groundCheck; // Position to check if the player is grounded
    public float groundCheckRadius = 0.2f; // Radius for ground check
    public LayerMask groundLayer; // Layer that defines what counts as ground

    private Rigidbody2D rb;       // Reference to the Rigidbody2D component
    private Vector2 movement;     // Store movement input
    private bool isGrounded;      // Is the player on the ground?
    private bool canJump = true;  // Can the player jump?

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Get the Rigidbody2D component
    }

    void Update()
    {
        // Get input from arrow keys or WASD for horizontal movement
        movement.x = Input.GetAxisRaw("Horizontal");

        // Check if the player is grounded using a Physics2D overlap
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Reset jump ability when grounded
        if (isGrounded)
        {
            canJump = true;  // Player can jump again
        }

        // Jump only when the player is grounded and presses the Jump button (space by default)
        if (isGrounded && Input.GetButtonDown("Jump") && canJump)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            canJump = false;  // Disable jumping again until grounded
        }
    }

    void FixedUpdate()
    {
        // Apply horizontal movement
        rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);
    }
}
