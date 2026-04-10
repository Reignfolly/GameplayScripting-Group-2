using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementWithDash : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 6f;        // How fast the player moves normally
    public float acceleration = 20f;    // How quickly player reaches target speed (smoothness)

    [Header("Dash Settings")]
    public float dashSpeed = 20f;       // Speed during dash
    public float dashDuration = 0.2f;   // How long the dash lasts
    public float dashCooldown = 1.5f;   // Time before player can dash again

    // References
    private Rigidbody rb;                // Player physics body
    private PlayerInputHandler input;    // Handles input (WASD, dash, etc.)

    // Dash state tracking
    private bool canDash = true;        // Can player currently dash?
    private bool isDashing = false;     // Is player currently dashing?
    private Vector3 dashDirection;      // Direction of dash

    // Used by SmoothDamp to store velocity smoothing data
    private Vector3 currentVelocity;

    void Awake()
    {
        // Get components on this object
        rb = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInputHandler>();

        // Prevent player from tipping over due to physics
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        // Smooth movement between physics frames
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        // Improves collision detection at higher speeds
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    void FixedUpdate()
    {
        // FixedUpdate is used for physics-based movement

        if (isDashing)
        {
            // DASH MOVEMENT:
            // Override normal movement and move quickly in dash direction
            // BUT keep Y velocity so gravity still works

            rb.linearVelocity = new Vector3(
                dashDirection.x * dashSpeed,
                rb.linearVelocity.y,
                dashDirection.z * dashSpeed
            );
        }
        else
        {
            // NORMAL MOVEMENT:

            // Get input direction (WASD / controller stick)
            Vector3 targetDir = new Vector3(input.MoveInput.x, 0f, input.MoveInput.y);

            // Prevent faster diagonal movement
            if (targetDir.magnitude > 1f)
                targetDir.Normalize();

            // Convert direction into velocity
            Vector3 targetVelocity = targetDir * moveSpeed;

            // Preserve gravity (Y velocity)
            targetVelocity.y = rb.linearVelocity.y;

            // Smooth movement (prevents instant snapping)
            rb.linearVelocity = Vector3.SmoothDamp(
                rb.linearVelocity,
                targetVelocity,
                ref currentVelocity,
                1f / acceleration
            );
        }
    }

    void Update()
    {
        // Update is used for input detection (not physics)

        // Check if player pressed dash and dash is available
        if (input.DashPressed && canDash)
        {
            StartCoroutine(DashRoutine()); // Start dash sequence
            input.ResetDash();             // Prevent repeated triggering
        }
        else if (!canDash && input.DashPressed)
        {
            // Ignore dash input during cooldown
            input.ResetDash();
        }
    }

    IEnumerator DashRoutine()
    {
        // Disable dash availability
        canDash = false;
        isDashing = true;

        // Determine dash direction based on input
        Vector3 moveInput = new Vector3(input.MoveInput.x, 0f, input.MoveInput.y);

        // If no movement input, dash forward
        dashDirection = moveInput.sqrMagnitude > 0.01f
            ? moveInput.normalized
            : transform.forward;

        // Wait for dash duration (how long dash lasts)
        yield return new WaitForSeconds(dashDuration);

        // End dash state
        isDashing = false;

        // Wait for cooldown before allowing next dash
        yield return new WaitForSeconds(dashCooldown);

        // Re-enable dash
        canDash = true;
    }

    // External scripts can check if player is dashing
    public bool IsDashing() => isDashing;
}