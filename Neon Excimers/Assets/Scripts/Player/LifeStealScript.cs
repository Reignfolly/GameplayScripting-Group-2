using UnityEngine;
using UnityEngine.Events;

public class LifeStealScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public PlayerStats PlayerStats;
    public Health_Module PlayerHealthModule;

    UnityEvent PlayerKilledEnemyEvent;
    void Start()
    {
        if (PlayerKilledEnemyEvent == null)
        {
            PlayerKilledEnemyEvent = new UnityEvent();
        }

        PlayerKilledEnemyEvent.AddListener(GiveMeHealth);
    }

    // Update is called once per frame
    public void GiveMeHealth()
    {
        PlayerHealthModule.TakeDamage(-1);
        Debug.Log("Orbi has reaped yet another silicon soul.");
    }
}
