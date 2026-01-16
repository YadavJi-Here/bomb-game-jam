using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 8f;
    public float jumpPower = 12f;

    [Header("Ground Detection")]
    public Transform groundCheck;    // Assign an empty GameObject located at the player's feet
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;    // Select the layer used for your ground/platforms

    private Rigidbody2D rb;
    private float horizontalInput;
    private bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 1. Capture Input (Processed every frame for responsiveness)
        // GetAxisRaw makes movement snappy (stops immediately when key is released)
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // 2. Handle Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            PerformJump();
        }
    }

    void FixedUpdate()
    {
        // 3. Apply Physics (Processed in fixed time intervals)

        // Move the player: Keep current Y velocity, change X velocity
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);

        // Check if player is touching the ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    void PerformJump()
    {
        // Reset Y velocity before jumping to ensure consistent jump height
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
    }

    // Visualization for the editor to help you adjust the ground check
    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}