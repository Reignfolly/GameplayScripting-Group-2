using UnityEngine;

public class Health_Module : MonoBehaviour
{

    // The maximum amount of health this character has
    public int Max_Health = 100;
    // The character's live, current health
    public int Current_Health = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void TakeDamage(int Damage)
    {
        // Subtracts Health from this character
        Current_Health -= Damage;
    }

    void SetHealth(int NewHealth)
    {
        // Set's character's health to the exact amount specified
        Current_Health = NewHealth;
    }

    int GetHealth()
    {
        // Returns this character's current Health
        return Current_Health;
    }

    void EnforceMaximumHealth()
    {
        // Prevents characters from having more health than they should
        if (Current_Health > Max_Health)
        {
            Current_Health = Max_Health;
        }
    }
}
