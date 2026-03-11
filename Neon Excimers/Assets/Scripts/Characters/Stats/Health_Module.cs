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

    public void TakeDamage(int Damage)
    {
        // Subtracts Health from this character
        Current_Health -= Damage;
        Debug.Log("New Health: " + Current_Health);
        HealthStatus_Update();
    }

    public void SetHealth(int NewHealth)
    {
        // Set's character's health to the exact amount specified
        Current_Health = NewHealth;
        HealthStatus_Update();
    }

    public int GetHealth()
    {
        // Returns this character's current Health
        return Current_Health;
    }

    public void EnforceMaximumHealth()
    {
        // Prevents characters from having more health than they should
        if (Current_Health > Max_Health)
        {
            Current_Health = Max_Health;
        }
    }

    public void HealthStatus_Update()
    {
        // Allows us to check if the character should be dead or whatnot
        // Use this function to add health or damage Visual FX: this will be called everytime
        // the player takes damage or has health set.
        EnforceMaximumHealth();
        if (Current_Health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        // Runs when character dies. That's it.
        // Kills the character. 
        Destroy(gameObject);
    }
}
