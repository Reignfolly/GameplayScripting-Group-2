using UnityEngine;

[CreateAssetMenu(fileName = "AI_Manager_Prototyping", menuName = "Scriptable Objects/AI_Manager_Prototyping")]
public class AI_Manager_Prototyping : ScriptableObject
{

    public int Maximum_Number_Of_Enemies = 12;
    public int Spawns_Per_Wave = 8;
    public int Wave_Timer = 15;
    public GameObject Clanker;

    private int Current_Number_Of_Enemies = 0;
    void Spawn_Until_Max()
    {
        // Simply spawns enemies until the max number is reached
        while (Current_Number_Of_Enemies < Maximum_Number_Of_Enemies)
        {

        }
    }

    void Spawn_Set_Number()
    {
        // Spawns enemies until either the max number of enemies are reached
        // or spawns the full amount of given enemies.
    }

    void Spawn_Enemy()
    {
        // Spawns an enemy at a random available spawn point
        // !! NOTE !! For now this script just finds the player's location and spawns the enemy directly on top of them

        var ChosenSpawnPosition = GameObject.Find("Player").transform.position;
        Instantiate(Clanker, ChosenSpawnPosition, Quaternion.identity);
    }

    void Destroy_All_Spawned_Enemies()
    {
        // Deletes all enemies spawned in.
    }
}
