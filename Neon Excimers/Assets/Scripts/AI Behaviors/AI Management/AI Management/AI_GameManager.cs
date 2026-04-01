using UnityEngine;
using System;
using System.Collections.Generic;

public class AI_GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int Maximum_Number_Of_Enemies = 12;
    public int Spawns_Per_Wave = 8;
    public int Wave_Timer = 15;
    const int Debug_InfiniteSpawn_Timer_Default = 10;
    public float Debug_InfiniteSpawn_Timer = 10f;
    public GameObject Clanker;

    private int Current_Number_Of_Enemies = 0;

    public int Spawn_X_Radius_rand = 40;
    public int Spawn_Z_Radius_rand = 40;
    public List<UnityEngine.GameObject> List_Of_Enemies = new List<UnityEngine.GameObject>();
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug_InfiniteSpawn_Timer -= Time.deltaTime;
        if (Debug_InfiniteSpawn_Timer <= 0)
        {
            Debug_InfiniteSpawn_Timer = Debug_InfiniteSpawn_Timer_Default;
            Spawn_Enemy();
        }
    }
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

    public void Spawn_Enemy()
    {
        // Spawns an enemy at a random available spawn point
        // !! NOTE !! For now this script just finds the player's location 
        // and spawns them in a radius around them. 
        var x_Offset = UnityEngine.Random.Range(-Spawn_X_Radius_rand, Spawn_X_Radius_rand);
        var z_Offset = UnityEngine.Random.Range(-Spawn_Z_Radius_rand, Spawn_Z_Radius_rand);
        var ChosenSpawnPosition = GameObject.Find("Player").transform.position;
        Instantiate(Clanker, new Vector3((ChosenSpawnPosition.x + x_Offset), 12f, (ChosenSpawnPosition.z + z_Offset)), Quaternion.identity);
    }

    public void Add_Enemy_To_List(GameObject EnemyInQuestion)
    {
        // Fairly straightforward - Just adds them into the list
        Current_Number_Of_Enemies += 1;
        List_Of_Enemies.Add(EnemyInQuestion);
    }

    public void Remove_Enemy_From_List(GameObject EnemyInQuestion)
    {
        // Fairly straightforward - Just removes them from the list (because they're dead)
        Current_Number_Of_Enemies -= 1;
        List_Of_Enemies.Remove(EnemyInQuestion);
    }

    void Destroy_All_Spawned_Enemies()
    {
        // Deletes all enemies spawned in.

        // This should be the last thing that you do.
        List_Of_Enemies.Clear();
    }
}
