using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerRotation : MonoBehaviour
{
    private Camera mainCamera;
    private PlayerInputHandler input;
    private Rigidbody rb;

    void Awake()
    {
        mainCamera = Camera.main;
        input = GetComponent<PlayerInputHandler>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        RotateTowardsMouse();
    }

    void RotateTowardsMouse()
    {
        // Create a ray from camera to mouse position
        Ray ray = mainCamera.ScreenPointToRay(input.MousePosition);

        // Create a flat plane at player's height
        Plane groundPlane = new Plane(Vector3.up, transform.position);

        // Check if ray hits plane
        if (groundPlane.Raycast(ray, out float distance))
        {
            // Get world point where mouse hits plane
            Vector3 worldPoint = ray.GetPoint(distance);

            // Get direction from player to mouse
            Vector3 direction = (worldPoint - transform.position).normalized;

            direction.y = 0f; // Prevent vertical tilt

            // Only rotate if direction is valid
            if (direction.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                // Smooth rotation
                rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, 10f * Time.fixedDeltaTime));
            }
        }
    }
}