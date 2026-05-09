using UnityEngine;
using TMPro;

public class StatDisplay : MonoBehaviour
{
    public PlayerStats PlayerStats;
    public WeaponStats WeaponStats;
    public Health_Module healthModule;

    public TMP_Text StatText;

    [Header("Player Stat Modifiers")]
    public float moveSpeedModifier = 0f;
    //public float accelerationModifier = 0f;
    public float dashSpeedModifier = 0f;
    //public float dashDurationModifier = 0f;
    public float dashCooldownModifier = 0f;

    [Header("Weapon Stat Modifiers")]

    public float rangeModifier = 0f;
    public float damageModifier = 0f;
    //public float areaModifier = 0f;
    public float attackSpeedModifier = 0f;

    [Header("Health Stat Modifiers")]
    public float healthModifier = 0f;


    public void UpdateStatsDisplay()
    {   
        damageModifier = WeaponStats.damageModifier;
        attackSpeedModifier = WeaponStats.attackSpeedModifier;
        rangeModifier = WeaponStats.rangeModifier;
        moveSpeedModifier = PlayerStats.moveSpeedModifier;
        dashCooldownModifier = PlayerStats.dashCooldownModifier;
        healthModifier = healthModule.Max_Health/100f * 100f - 100f;

        if (StatText != null)
        {   
           StatText.text = $"Attack Damage: ({(WeaponStats.damageModifier >= 0 ? "+" : "")}{WeaponStats.damageModifier}%)\n" +
                           $"Attack Speed: ({(WeaponStats.attackSpeedModifier >= 0 ? "+" : "")}{WeaponStats.attackSpeedModifier}%)\n" +
                           $"Attack Range: ({(WeaponStats.rangeModifier >= 0 ? "+" : "")}{WeaponStats.rangeModifier}%)\n" +
                           $"Move Speed: ({(PlayerStats.moveSpeedModifier >= 0 ? "+" : "")}{PlayerStats.moveSpeedModifier}%)\n" +
                           $"Dash Cooldown: ({(PlayerStats.dashCooldownModifier >= 0 ? "+" : "")}{PlayerStats.dashCooldownModifier}%)\n" +
                           $"Health: ({(healthModifier >= 0 ? "+" : "")}{healthModifier}%)";
             

        }
    }
    void Update()
    {   
        
    }
}
