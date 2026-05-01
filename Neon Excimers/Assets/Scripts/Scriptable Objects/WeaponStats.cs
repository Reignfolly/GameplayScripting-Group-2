using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStats", menuName = "Scriptable Objects/WeaponStats")]
public class WeaponStats : ScriptableObject
{
    [Header("Base Stats")]
    public float baseRange = 50f;
    public float baseDamage = 50f;
    public float baseArea = 0.2f;

    public float baseAttackSpeed = 0.1f;

    [Header("Stat Modifiers")]
    public float rangeModifier = 0f;
    public float damageModifier = 0f;
    public float areaModifier = 0f;
    public float attackSpeedModifier = 0f;

    [Header("Calculated Stats (Read-Only)")]
    public float range { get; private set; }
    public float damage { get; private set; }
    public float area { get; private set; }

    public float attackSpeed { get; private set; }

    void OnEnable()
    {   

        UpgradeRefresh();
    }

    public void UpgradeRefresh ()
    {   
         // This function will be called whenever an upgrade is purchased to recalculate the weapon's stats based on the base values and the upgrades they have.

        range = baseRange * Mathf.Max(1f, 1f + (rangeModifier / 100f));
        damage = baseDamage * Mathf.Max(1f, 1f + (damageModifier / 100f));
        area = baseArea * Mathf.Max(0.1f, 1f + (areaModifier / 100f));
        attackSpeed = baseAttackSpeed / Mathf.Max(0.01f, 1.0f + (attackSpeedModifier / 100.0f));
    }
}
