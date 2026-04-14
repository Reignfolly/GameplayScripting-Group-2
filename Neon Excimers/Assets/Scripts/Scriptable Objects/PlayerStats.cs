using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
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
        moveSpeedModifier = 0f;
        accelerationModifier = 0f;
        dashSpeedModifier = 0f;
        dashDurationModifier = 0f;
        dashCooldownModifier = 0f;
        UpgradeRefresh();
    }


        void UpgradeRefresh ()
    {   
        
        // This function will be called whenever an upgrade is purchased to recalculate the player's stats based on the base values and the upgrades they have.

        moveSpeed = baseMoveSpeed * (1 + moveSpeedModifier/100);
        acceleration = baseAcceleration * (1 + accelerationModifier/100);
        dashSpeed = baseDashSpeed * (1 + dashSpeedModifier/100);
        dashDuration = baseDashDuration * (1 + dashDurationModifier/100);
        dashCooldown = baseDashCooldown * (1 - dashCooldownModifier/100);
    }
}
