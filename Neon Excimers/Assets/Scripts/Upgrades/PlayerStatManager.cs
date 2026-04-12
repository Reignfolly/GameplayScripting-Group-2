using UnityEngine;

public class PlayerStatManager : MonoBehaviour
{   
    public static PlayerStatManager instance {get; private set;} 

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this);
        } else {
            instance = this;
        }
    }

    //Movement base stats
    private float baseMoveSpeed = 6f;        // How fast the player moves normally
    private float baseAcceleration = 20f;    // How quickly player reaches target speed (smoothness)
    //Useable stats
    public float moveSpeed {get; private set;}
    public float acceleration {get; private set;}

    //Dash base stats
    private float baseDashSpeed = 20f;       // Speed during dash
    private float baseDashDuration = 0.2f;   // How long the dash lasts
    private float baseDashCooldown = 1.5f;  

    //Useable stats
    public float dashSpeed {get; private set;}
    public float dashDuration {get; private set;}
    public float dashCooldown {get; private set;}

    //laser base stats
    private float baserange = 25f;
    private float basedamage = 50f;
    private float baseduration = 0.1f;
    private float basewidth = 0.2f;

    //Useable stats
    public float range {get; private set;}
    public float damage {get; private set;}
    public float duration {get; private set;}
    public float width {get; private set;}


       void Start()
    {
        // Initialize useable stats to base stats at the start of the game
        moveSpeed = baseMoveSpeed;
        acceleration = baseAcceleration;

        dashSpeed = baseDashSpeed;
        dashDuration = baseDashDuration;
        dashCooldown = baseDashCooldown;

        range = baserange;
        damage = basedamage;
        duration = baseduration;
        width = basewidth;
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    void UpgradeRefresh ()
    {
        // This function will be called whenever an upgrade is purchased to recalculate the player's stats based on the base values and the upgrades they have.
        // For example:
        // moveSpeed = baseMoveSpeed * (1 + moveSpeedUpgradePercentage);
        // dashSpeed = baseDashSpeed * (1 + dashSpeedUpgradePercentage);
        // And so on for each stat...
    }
}
