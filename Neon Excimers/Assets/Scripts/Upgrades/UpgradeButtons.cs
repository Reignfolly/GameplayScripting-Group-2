using UnityEngine;

public class UpgradeButtons : MonoBehaviour
{
    public PlayerStats playerStats;
    public WeaponStats weaponStats;

    public LaserShooter laserShooter;

    public Health_Module healthModule;

     void Start()
    {
        if (playerStats == null)
        {
            Debug.LogError("PlayerStats reference is missing on UpgradeButtons!");
        }
    }

        public void RefreshStats()
    {

            playerStats.UpgradeRefresh();
            weaponStats.UpgradeRefresh();
            laserShooter.UpdateGunStats();

    }
        public void FullHeal()
    {
        healthModule.Current_Health = healthModule.Max_Health;
    }

    //===================Player stat upgrade functions========
        public void Upgrade_Damage()
    {
        weaponStats.damageModifier += 15f;
    }
    public void Upgrade_AttackSpeed()
    {
        weaponStats.attackSpeedModifier += 15f;
    }
    public void Upgrade_MoveSpeed()
    {
       playerStats.moveSpeedModifier += 15f; 
    }

    public void Upgrade_Balance()
    {
        weaponStats.damageModifier += 5f;
        weaponStats.attackSpeedModifier += 5f;
        weaponStats.rangeModifier += 5f;
        playerStats.moveSpeedModifier += 5f;
        
    }

    public void Upgrade_Focus()
    {
        weaponStats.damageModifier += 20f;
        weaponStats.attackSpeedModifier -= 5f;
        //weaponStats.knockbackModifier += 5f;
        weaponStats.rangeModifier += 5f;
    }
    public void Upgrade_Disperse()
    {
        weaponStats.attackSpeedModifier += 20f;
        weaponStats.damageModifier -= 5f;
        weaponStats.rangeModifier += 5f;
    }

    public void Upgrade_Swiftfoot()
    {
        playerStats.moveSpeedModifier += 10f;
        playerStats.dashCooldownModifier += 10f;
    }

    public void Upgrade_Vitality()
    {
        healthModule.Max_Health += 15;
    }

    public void Upgrade_Tank()
    {

        healthModule.Max_Health += 30;
        playerStats.moveSpeedModifier -= 20f;
    }

    public void Upgrade_Sharpshooter()
    {
        weaponStats.rangeModifier += 30f;
    }

    public void Upgrade_CloseQuarters()
    {
        weaponStats.damageModifier += 75f;
        weaponStats.rangeModifier -= 50f;
    }

    public void Upgrade_Windstep()
    {   

        playerStats.dashCooldownModifier += 20f;

    }

    public void Upgrade_Burst()
    {
        weaponStats.attackSpeedModifier += 10f;
        playerStats.dashCooldownModifier += 10f;
    }

    public void Upgrade_Harmony()
    {
        weaponStats.damageModifier += 5f;
        weaponStats.attackSpeedModifier += 5f;
        weaponStats.rangeModifier += 5f;
        playerStats.moveSpeedModifier += 5f;
        playerStats.dashCooldownModifier += 5f;
        healthModule.Max_Health += 5;
        //knockback +5% (not implemented yet)
    }

    public void Upgrade_Impact()
    {
        //weaponStats.knockbackModifier += 30f; (not implemented yet)
    }

    public void Upgrade_Planted()
    {   
        weaponStats.damageModifier += 10f;
        weaponStats.attackSpeedModifier += 10f;
        weaponStats.rangeModifier += 10f;
        playerStats.moveSpeedModifier -= 10f;
    }

    public void Upgrade_GlassCannon()
    {
        weaponStats.damageModifier += 500f;
        healthModule.Max_Health -= 90;
    }

    public void Upgrade_GlassDancer()
    {
        playerStats.moveSpeedModifier += 500f;
        healthModule.Max_Health -= 90;
    }

    public void Upgrade_SlugFire()
    {
        weaponStats.damageModifier += 100f;
        weaponStats.attackSpeedModifier -= 70f;
    }
        public void Upgrade_SprayFire()
    {
        weaponStats.attackSpeedModifier += 100f;
        weaponStats.damageModifier -= 70f;
    }
}
