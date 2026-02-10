using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public Material playerMaterial;
    public float chrome = 1f;
    void OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.collider.tag == "Obstacle")
        {
            Debug.Log("We hit an obstacle!");
            chrome -= 0.025f;
            playerMaterial.SetFloat("_Metallic", chrome);
            if (chrome <= 0f)
            {
                Debug.Log("Player defeated!");
                // Additional defeat logic can be added here
            }
        }

    }
}
