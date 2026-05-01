using UnityEngine;
using TMPro;

public class StatDisplay : MonoBehaviour
{
    public PlayerStats PlayerStats;
    public WeaponStats WeaponStats;

    public TMP_Text StatText;
    void Update()
    {   
        if (StatText != null)
        {   
           StatText.text = 
    $"Attack Dmg: {WeaponStats.damage} ({WeaponStats.baseDamage} + {WeaponStats.damageModifier}%)\n" +
    $"Attack Range: {WeaponStats.range} ({WeaponStats.baseRange} + {WeaponStats.rangeModifier}%)\n" +
    $"Move Speed: {PlayerStats.moveSpeed} ({PlayerStats.baseMoveSpeed} + {PlayerStats.moveSpeedModifier}%)\n" +
    $"Dash Cdn: {PlayerStats.dashCooldown} ({PlayerStats.baseDashCooldown} - {PlayerStats.dashCooldownModifier}%)";
             

        }
        
    }
}
