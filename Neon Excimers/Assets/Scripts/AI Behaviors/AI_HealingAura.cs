using UnityEngine;

public class AI_HealingAura : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private bool HealingDebounce;
    const int Default_HealingTimer = 1;
    public float Current_HealingTimer = 1f;
    void Start()
    {
        HealingDebounce = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Current_HealingTimer <= 0)
        {
            Current_HealingTimer = Default_HealingTimer;
            HealingDebounce = false;
        }

        if (HealingDebounce == true)
        {
            Current_HealingTimer -= Time.deltaTime;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (HealingDebounce == false)
        {
            Debug.Log("I began healing another enemy!");
            HealingDebounce = true;
            if (other.gameObject.tag == "Enemy")
            {
                var EnemyHealthModule = other.gameObject.GetComponent<Health_Module>();
                // Negative values means healing
                EnemyHealthModule.TakeDamage(-5);
            }

            Debug.Log("I collided with: " + other.gameObject.name);
        }
    }

    void OnCollisionStay(Collision other)
    {
        if (HealingDebounce == false)
        {
            Debug.Log("I am still healing another enemy!");
            HealingDebounce = true;
            if (other.gameObject.tag == "Enemy")
            {
                var EnemyHealthModule = other.gameObject.GetComponent<Health_Module>();
                // Negative values means healing
                EnemyHealthModule.TakeDamage(-5);
            }

            Debug.Log("I collided with: " + other.gameObject.name);
        }
    }
}
