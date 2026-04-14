using UnityEngine;

public class UpgradeButtons : MonoBehaviour
{
    public PlayerStats playerStats;
    public WeaponStats weaponStats;

     void Start()
    {
        if (playerStats == null)
        {
            Debug.LogError("PlayerStats reference is missing on UpgradeButtons!");
        }
    }

    //===================Player stat upgrade functions========
    public void UpgradeMoveSpeed()
    {
       playerStats.moveSpeedModifier += 15f; 
    }

     public void UpgradeDashCooldown()
    {   
        if (playerStats.dashCooldownModifier >= 80f) {
            Debug.Log("Dash cooldown cannot be reduced further!");
            return;
        } else {
            playerStats.dashCooldownModifier += 15f;
        }
    }

    //===================Weapon stat upgrade functions========
    public void UpgradeDamage()
    {
        weaponStats.damageModifier += 15f;
    }

    public void UpgradeRange()
    {
        weaponStats.rangeModifier += 15f;
    }

    
}
