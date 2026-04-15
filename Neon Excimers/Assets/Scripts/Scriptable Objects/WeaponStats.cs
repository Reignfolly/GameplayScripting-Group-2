using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStats", menuName = "Scriptable Objects/WeaponStats")]
public class WeaponStats : ScriptableObject
{
    [Header("Base Stats")]
    public float baseRange = 50f;
    public float baseDamage = 50f;
    public float baseArea = 0.2f;

    [Header("Stat Modifiers")]
    public float rangeModifier = 0f;
    public float damageModifier = 0f;
    public float areaModifier = 0f;

    [Header("Calculated Stats (Read-Only)")]
    public float range { get; private set; }
    public float damage { get; private set; }
    public float area { get; private set; }

    void OnEnable()
    {   

        UpgradeRefresh();
    }

    void UpgradeRefresh ()
    {   
         // This function will be called whenever an upgrade is purchased to recalculate the weapon's stats based on the base values and the upgrades they have.

        range = range * (1 + rangeModifier/100f);
        damage = damage * (1 + damageModifier/100f);
        area = area * (1 + areaModifier/100f);
    }
}
