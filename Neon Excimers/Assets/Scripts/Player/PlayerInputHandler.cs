using UnityEngine;
using UnityEngine.InputSystem;

// This script ONLY handles input and stores it.
// Other scripts will read from this.
public class PlayerInputHandler : MonoBehaviour
{
    // Stores WASD / stick movement
    public Vector2 MoveInput { get; private set; }

    // Stores mouse screen position
    public Vector2 MousePosition { get; private set; }

    // Stores whether dash was pressed this frame
    public bool DashPressed { get; private set; }

    // Called automatically by the Input System when Move is triggered
    public void OnMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }

    // Called automatically when Look action is triggered
    public void OnLook(InputAction.CallbackContext context)
    {
        MousePosition = context.ReadValue<Vector2>();
    }

    // Called when Dash input is triggered
    public void OnDash(InputAction.CallbackContext context)
    {
        // We only care when the button is first pressed
        if (context.started)
        {
            DashPressed = true;
        }
    }

    // Allows other scripts to reset dash after using it
    public void ResetDash()
    {
        DashPressed = false;
    }
}