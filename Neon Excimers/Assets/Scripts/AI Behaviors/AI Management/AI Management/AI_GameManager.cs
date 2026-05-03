using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

public class AI_GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int Maximum_Number_Of_Enemies = 12;
    public int Spawns_Per_Wave = 8;
    public int Wave_Timer = 15;
    const int Debug_InfiniteSpawn_Timer_Default = 10;
    public float Debug_InfiniteSpawn_Timer = 10f;
    public GameObject Clanker;

    public GameObject Medic;

    public GameObject Bulldozer;
    public GameObject Ranger;
    public GameObject Shark;

    public int Current_Number_Of_Enemies = 0;

    public int Spawn_X_Radius_rand = 120;
    public int Spawn_Z_Radius_rand = 120;
    public List<UnityEngine.GameObject> List_Of_Enemies = new List<UnityEngine.GameObject>();
    public List<UnityEngine.GameObject> List_Of_SpawnPoints = new List<UnityEngine.GameObject>();

    // 1 in this chance for a medic to spawn
    public int ChanceForMedic = 10;
    public int ChanceForBulldozer = 30;
    public int ChanceForRanger = 5;

    public int ChanceForShark = 4;

    public PlayerStats PlayerStats;
    public UnityEvent PlayerKilledEnemyEvent = new UnityEvent();
    void Start()
    {
        if (PlayerKilledEnemyEvent == null)
        {
            PlayerKilledEnemyEvent = new UnityEvent();
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*Debug_InfiniteSpawn_Timer -= Time.deltaTime;
        if (Debug_InfiniteSpawn_Timer <= 0)
        {
            Debug_InfiniteSpawn_Timer = Debug_InfiniteSpawn_Timer_Default;
            Spawn_Enemy();
        }*/
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

    public void Spawn_Enemy(Enemy_Types NewEnemyToSpawn)
    {
        // Spawns an enemy at a random available spawn point
        // !! NOTE !! For now this script just finds the player's location 
        // and spawns them in a radius around them. 
        var WhatAmI = UnityEngine.Random.Range(1, ChanceForBulldozer + 1);
        var x_Offset = UnityEngine.Random.Range(-Spawn_X_Radius_rand, Spawn_X_Radius_rand);
        var z_Offset = UnityEngine.Random.Range(-Spawn_Z_Radius_rand, Spawn_Z_Radius_rand);

        var NumberIn_SpawnPointList = UnityEngine.Random.Range(0, List_Of_SpawnPoints.Count);
        var SpawnPointToSelect = List_Of_SpawnPoints[NumberIn_SpawnPointList].transform.position;

        switch (NewEnemyToSpawn)
        {
            case Enemy_Types.Standard:
                // Standard enemy type
                Instantiate(Clanker, new Vector3(SpawnPointToSelect.x + x_Offset, 8f, SpawnPointToSelect.z + z_Offset), Quaternion.identity);
                break;
            case Enemy_Types.Ranger:
                // Ranger enemy type
                Instantiate(Ranger, new Vector3(SpawnPointToSelect.x + x_Offset, 8f, SpawnPointToSelect.z + z_Offset), Quaternion.identity);
                break;
            case Enemy_Types.Shark:
                // Shark enemy type
                Instantiate(Shark, new Vector3(SpawnPointToSelect.x + x_Offset, 8f, SpawnPointToSelect.z + z_Offset), Quaternion.identity);
                break;
            case Enemy_Types.Bulldozer:
                // Bulldozer enemy type
                Instantiate(Bulldozer, new Vector3(SpawnPointToSelect.x + x_Offset, 8f, SpawnPointToSelect.z + z_Offset), Quaternion.identity);
                break;
            case Enemy_Types.Medic:
                // Medic enemy type
                Instantiate(Medic, new Vector3(SpawnPointToSelect.x + x_Offset, 8f, SpawnPointToSelect.z + z_Offset), Quaternion.identity);
                break;
            case Enemy_Types.Officer:
                // Officer enemy type
                break;
        }
    }

    public void Add_Enemy_To_List(GameObject EnemyInQuestion)
    {
        // Fairly straightforward - Just adds them into the list
        Current_Number_Of_Enemies += 1;
        List_Of_Enemies.Add(EnemyInQuestion);
        var Wave_Manager_Script = this.gameObject.GetComponentInChildren<WaveManager_Script>();
        Wave_Manager_Script.Current_Number_Of_Enemies = Current_Number_Of_Enemies;
    }

    public void Remove_Enemy_From_List(GameObject EnemyInQuestion)
    {
        // Fairly straightforward - Just removes them from the list (because they're dead)
        Current_Number_Of_Enemies -= 1;
        List_Of_Enemies.Remove(EnemyInQuestion);

        var Wave_Manager_Script = this.gameObject.GetComponentInChildren<WaveManager_Script>();
        Wave_Manager_Script.Current_Number_Of_Enemies = Current_Number_Of_Enemies;

        PlayerStats.KillCount += 1;
        PlayerKilledEnemyEvent.Invoke();
    }

    public List<UnityEngine.GameObject> GiveMeEnemyList()
    {
        return List_Of_Enemies;
    }

    void Destroy_All_Spawned_Enemies()
    {
        // Deletes all enemies spawned in.

        // This should be the last thing that you do.
        List_Of_Enemies.Clear();
    }

    public void Add_SpawnPoint_To_List(GameObject SpawnPointInQuestion)
    {
        // Fairly straightforward - Just adds them into the list
        List_Of_SpawnPoints.Add(SpawnPointInQuestion);
    }
}
