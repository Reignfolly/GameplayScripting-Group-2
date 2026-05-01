using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats")]
public class PlayerStats : ScriptableObject
{   
    public int KillCount = 0;

    [Header("Base Stats")]
    public float baseMoveSpeed = 6f;        // How fast the player moves normally
    public float baseAcceleration = 20f;    // How quickly player reaches target speed (smoothness)
    public float baseDashSpeed = 20f;       // Speed during dash
    public float baseDashDuration = 0.2f;   // How long the dash lasts
    public float baseDashCooldown = 1.5f;  

    [Header("Stat Modifiers")]
    public float moveSpeedModifier = 0f;
    public float accelerationModifier = 0f;
    public float dashSpeedModifier = 0f;
    public float dashDurationModifier = 0f;
    public float dashCooldownModifier = 0f;

    [Header("Calculated Stats (Read-Only)")]
    public float moveSpeed { get; private set; }
    public float acceleration { get; private set; }
    public float dashSpeed { get; private set; }
    public float dashDuration { get; private set; }
    public float dashCooldown { get; private set; }

    void OnEnable()
    {   

        UpgradeRefresh();
    }


    public void UpgradeRefresh ()
    {   
        
        // This function will be called whenever an upgrade is purchased to recalculate the player's stats based on the base values and the upgrades they have.

        moveSpeed = baseMoveSpeed * Mathf.Max(0.1f, 1f + (moveSpeedModifier / 100f));
        acceleration = baseAcceleration * Mathf.Max(0f, 1f + (accelerationModifier / 100f));
        dashSpeed    = baseDashSpeed    * Mathf.Max(0f, 1f + (dashSpeedModifier / 100f));
        dashDuration = baseDashDuration * Mathf.Max(0f, 1f + (dashDurationModifier / 100f));
        dashCooldown = baseDashCooldown / Mathf.Max(0.01f, 1.0f + (dashCooldownModifier / 100.0f));
    }
}
