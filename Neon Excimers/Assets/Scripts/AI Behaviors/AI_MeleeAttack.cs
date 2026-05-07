using UnityEngine;

public class AI_MeleeAttack : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool AttackDebounce;
    public int Damage = 20;

    const float Default_Attack_Timer = .5f;
    public float Current_Attack_Timer = .5f;
    void Start()
    {
        AttackDebounce = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Current_Attack_Timer <= 0)
        {
            Current_Attack_Timer = Default_Attack_Timer;
            AttackDebounce = false;
        }

        if (AttackDebounce == true)
        {
            Current_Attack_Timer -= Time.deltaTime;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (AttackDebounce == false)
        {
            AttackDebounce = true;
            if (collision.gameObject.name == "Player")
            {
                var PlayerHealthModule = collision.gameObject.GetComponent<Health_Module>();
                //Debug.Log("I collided with the player!");
                PlayerHealthModule.TakeDamage(Damage);
            }
        ;
            //Debug.Log("I collided with: " + collision.gameObject.name);
        }
    }
}
