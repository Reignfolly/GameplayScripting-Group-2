using UnityEngine;

public class CharacterComponent : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public PlayerCharacterClass PlayerCharacterStats = new PlayerCharacterClass()
    {
        Name = "New Character", // Name
        Description = "Robot character.", // Description
        lifeSteal = 1,
        baseMoveSpeed = 6f,
        baseAcceleration = 20f,
        baseDashSpeed = 20f,
        baseDashDuration = .2f,
        baseDashCooldown = 1.5f,
        baseRange = 50f,
        baseDamage = 50f,
        baseArea = 0.2f,
        baseAttackSpeed = 0.1f,
    };
}
