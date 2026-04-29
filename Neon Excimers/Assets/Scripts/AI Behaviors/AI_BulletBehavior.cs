using UnityEngine;

public class AI_BulletBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float BulletTimeToLive = 10f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        BulletTimeToLive -= Time.deltaTime;
        if (BulletTimeToLive <= 0)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        //print("(EnemyBullet) I hit something!");
        if (other.gameObject.name == "Player")
        {
            var PlayerHealthModule = other.gameObject.GetComponent<Health_Module>();
            //Debug.Log("I collided with the player!");
            PlayerHealthModule.TakeDamage(15);
        }
        ;
        Destroy(gameObject);
    }
}
