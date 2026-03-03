using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementWithDash : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;           // Normal move speed
    public float acceleration = 20f;       // How fast player accelerates to target speed

    [Header("Dash")]
    public float dashSpeed = 20f;          // Dash speed
    public float dashDuration = 0.2f;      // Dash lasts this long
    public float dashCooldown = 1.5f;      // Time before dash can be used again

    private Rigidbody rb;
    private PlayerInputHandler input;

    // Dash state
    private bool canDash = true;
    private bool isDashing = false;
    private Vector3 dashDirection;

    // Smooth movement
    private Vector3 currentVelocity;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInputHandler>();

        // Freeze rotation X and Z so collisions don't tilt the player
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            // During dash, override velocity but preserve Y so gravity still works
            rb.linearVelocity = new Vector3(dashDirection.x * dashSpeed, rb.linearVelocity.y, dashDirection.z * dashSpeed);
        }
        else
        {
            // Normal movement
            Vector3 targetDir = new Vector3(input.MoveInput.x, 0f, input.MoveInput.y);

            // Normalize diagonal movement
            if (targetDir.magnitude > 1f)
                targetDir.Normalize();

            Vector3 targetVelocity = targetDir * moveSpeed;

            // Preserve Y-velocity so gravity works
            targetVelocity.y = rb.linearVelocity.y;

            // Smooth acceleration on X/Z, keep Y
            rb.linearVelocity = Vector3.SmoothDamp(rb.linearVelocity, targetVelocity, ref currentVelocity, 1f / acceleration);
        }
    }

    void Update()
    {
        // Dash input
        if (input.DashPressed && canDash)
        {
            StartCoroutine(DashRoutine());
            input.ResetDash();
        }
        else if (!canDash && input.DashPressed)
        {
            // Ignore input during cooldown
            input.ResetDash();
        }
    }

    IEnumerator DashRoutine()
    {
        canDash = false;
        isDashing = true;

        // Dash in movement direction, fallback to forward
        Vector3 moveInput = new Vector3(input.MoveInput.x, 0f, input.MoveInput.y);
        dashDirection = moveInput.sqrMagnitude > 0.01f ? moveInput.normalized : transform.forward;

        // Wait for dash duration
        yield return new WaitForSeconds(dashDuration);

        // End dash
        isDashing = false;

        // Start cooldown
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    // Other scripts can check if player is dashing
    public bool IsDashing() => isDashing;
}