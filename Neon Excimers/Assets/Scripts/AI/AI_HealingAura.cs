using UnityEngine;

public class AI_HealingAura : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private bool HealingDebounce;
    const float Default_HealingTimer = .1f;
    public float Current_HealingTimer = .1f;

    public GameObject HealFX;

    public int AmountToHeal = 10;
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

    void OnTriggerEnter(Collider other1)
    {
        HealContinually(other1);
    }

    void OnTriggerStay(Collider other)
    {
        HealWithCooldown(other);
    }

    /*void OnCollisionEnter(Collider other2)
    {
        HealContinually(other2);
    }

    void OnCollisionStay(Collision other)
    {
        HealWithCooldown(other);
    }*/



    void SpawnHealFX(Vector3 HitPoint)
    {
        GameObject NewHealFX = Instantiate(HealFX, HitPoint, Quaternion.identity);
    }



    void HealContinually(Collider other)
    {
        //Debug.Log("I began healing another enemy!");
        HealingDebounce = true;
        if (other.gameObject.tag == "Enemy")
        {
            var EnemyHealthModule = other.gameObject.GetComponent<Health_Module>();
            // Negative values means healing
            EnemyHealthModule.TakeDamage(-AmountToHeal);
            SpawnHealFX(other.gameObject.transform.position);
        }
        //Debug.Log("I collided with: " + other.gameObject.name);
    }


    void HealWithCooldown(Collider other)
    {
        if (HealingDebounce == false)
        {
            //Debug.Log("I am still healing another enemy!");
            HealingDebounce = true;
            if (other.gameObject.tag == "Enemy")
            {
                var EnemyHealthModule = other.gameObject.GetComponent<Health_Module>();
                // Negative values means healing
                EnemyHealthModule.TakeDamage(-AmountToHeal);
                SpawnHealFX(other.gameObject.transform.position);
            }

            //Debug.Log("I collided with: " + other.gameObject.name);
        }
    }
}
