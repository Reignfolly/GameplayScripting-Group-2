using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CharacterClasses : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class PlayerCharacterClass
{
    public string Name = "Super Robot Killer 9000"; // Name of the Difficulty Level 
    public string Description = "Eats Titanium and fiber optic cables for breakfast."; // Description for a menu
    
    // Player Stats Modifiers
    public int lifeSteal = 1;

    // Base Player stats
    [Header("Base Character Stats")]
    public float baseMoveSpeed = 6f;        // How fast the player moves normally
    public float baseAcceleration = 20f;    // How quickly player reaches target speed (smoothness)
    public float baseDashSpeed = 20f;       // Speed during dash
    public float baseDashDuration = 0.2f;   // How long the dash lasts
    public float baseDashCooldown = 1.5f;  

    // Modifier Stats
    public float moveSpeedModifier = 0f;
    public float accelerationModifier = 0f;
    public float dashSpeedModifier = 0f;
    public float dashDurationModifier = 0f;
    public float dashCooldownModifier = 0f;


    // Weapon Stats

    // Base Weapon Stats
     [Header("Base Weapon Stats")]
    public float baseRange = 50f;
    public float baseDamage = 50f;
    public float baseArea = 0.2f;

    public float baseAttackSpeed = 0.1f;

    // Weapon Modifier Stats
    public float rangeModifier = 0f;
    public float damageModifier = 0f;
    public float areaModifier = 0f;
    public float attackSpeedModifier = 0f;
}