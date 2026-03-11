using UnityEngine;

public class AI_MeleeAttack : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            var PlayerHealthModule = collision.gameObject.GetComponent<Health_Module>();
            Debug.Log("I collided with the player!");
            PlayerHealthModule.TakeDamage(30);
        }
        ;
        Debug.Log("I collided with: " + collision.gameObject.name);
    }
}
