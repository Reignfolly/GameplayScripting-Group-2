using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour, IEnemyTracking
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        var GameManagerHolder = GameObject.Find("GameManager");
        var AI_Manager_Script = GameManagerHolder.gameObject.GetComponent<AI_GameManager>();
        AI_Manager_Script.Add_Enemy_To_List(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void YouDied()
    {
        // Once this enemy dies, it needs to go ahead and tell the GameManager that it's dead.
        Debug.Log("I died!");
        var GameManagerHolder = GameObject.Find("GameManager");
        var AI_Manager_Script = GameManagerHolder.gameObject.GetComponent<AI_GameManager>();
        AI_Manager_Script.Remove_Enemy_From_List(this.gameObject);
    }

    void onDestroy()
    {
        Debug.Log("The Enemy has been destroyed");
        YouDied();
    }
}

public interface IEnemyTracking
{
    void YouDied();
}