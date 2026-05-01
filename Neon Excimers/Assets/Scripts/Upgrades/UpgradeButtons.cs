using UnityEngine;

public class UpgradeButtons : MonoBehaviour
{
    public PlayerStats playerStats;
    public WeaponStats weaponStats;

    public LaserShooter laserShooter;

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



    public void Upgrade_Sharpshooter()
    {
        weaponStats.rangeModifier += 30f;
    }

    public void Upgrade_Windstep()
    {   

        playerStats.dashCooldownModifier += 20f;

    }
}
