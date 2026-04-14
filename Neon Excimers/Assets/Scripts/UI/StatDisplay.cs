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

            StatText.text = "Attack Dmg: " + WeaponStats.damage.ToString() + " (" + WeaponStats.baseDamage + " + " + WeaponStats.damageModifier.ToString() + "%)\n"+
            "Attack Range: " + WeaponStats.range.ToString() + " (" + WeaponStats.baseRange + " + " + WeaponStats.rangeModifier.ToString() + "%)\n"+
            "Move Speed: " + PlayerStats.moveSpeed.ToString() + " (" + PlayerStats.baseMoveSpeed + " + " + PlayerStats.moveSpeedModifier.ToString() + "%)\n"+
            "Dash Cdn: " + PlayerStats.dashCooldown.ToString() + " (" + PlayerStats.baseDashCooldown + " - " + PlayerStats.dashCooldownModifier.ToString() + "%)";
             

        }
        
    }
}
