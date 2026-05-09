using UnityEngine;
using System;
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

    public int Spawn_X_Radius_rand = 350;
    public int Spawn_Z_Radius_rand = 350;

    public AI_SpawnModes AI_Spawn_Location_Mode;
    public List<UnityEngine.GameObject> List_Of_Enemies = new List<UnityEngine.GameObject>();
    public List<UnityEngine.GameObject> List_Of_SpawnPoints = new List<UnityEngine.GameObject>();
    public List<int> SpawnSafeZonePoints = new List<int>();

    public PlayerStats PlayerStats;
    public UnityEvent PlayerKilledEnemyEvent = new UnityEvent();
    void Start()
    {
        if (PlayerKilledEnemyEvent == null)
        {
            PlayerKilledEnemyEvent = new UnityEvent();
        }
        SpawnSafeZonePoints.Add(-100);
        SpawnSafeZonePoints.Add(-150);
        SpawnSafeZonePoints.Add(100);
        SpawnSafeZonePoints.Add(150);
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

    public void Spawn_Enemy(Enemy_Types NewEnemyToSpawn)
    {
        // Spawns an enemy at a random available spawn point
        // !! NOTE !! For now this script just finds the player's location 
        // and spawns them in a radius around them. 
        var x_Offset = UnityEngine.Random.Range(-Spawn_X_Radius_rand, Spawn_X_Radius_rand);
        var z_Offset = UnityEngine.Random.Range(-Spawn_Z_Radius_rand, Spawn_Z_Radius_rand);

        if (Math.Abs(x_Offset) <= 90)
        {
            var New_PresetXOffset = UnityEngine.Random.Range(0, SpawnSafeZonePoints.Count);
            x_Offset = SpawnSafeZonePoints[New_PresetXOffset];
        }

        if (Math.Abs(z_Offset) <= 90)
        {
            var New_PresetZOffset = UnityEngine.Random.Range(0, SpawnSafeZonePoints.Count);
            z_Offset = SpawnSafeZonePoints[New_PresetZOffset];
        }



        var NumberIn_SpawnPointList = UnityEngine.Random.Range(0, List_Of_SpawnPoints.Count);
        var SpawnPosition = List_Of_SpawnPoints[NumberIn_SpawnPointList].transform.position;

        switch (AI_Spawn_Location_Mode)
        {
            case AI_SpawnModes.AroundPlayer:
                SpawnPosition = GameObject.Find("Player").transform.position;
                break;
            case AI_SpawnModes.SpawnPoints:
                SpawnPosition = List_Of_SpawnPoints[NumberIn_SpawnPointList].transform.position;
                break;
        }


        switch (NewEnemyToSpawn)
        {
            case Enemy_Types.Standard:
                // Standard enemy type
                Instantiate(Clanker, new Vector3(SpawnPosition.x + x_Offset, 8f, SpawnPosition.z + z_Offset), Quaternion.identity);
                break;
            case Enemy_Types.Ranger:
                // Ranger enemy type
                Instantiate(Ranger, new Vector3(SpawnPosition.x + x_Offset, 8f, SpawnPosition.z + z_Offset), Quaternion.identity);
                break;
            case Enemy_Types.Shark:
                // Shark enemy type
                Instantiate(Shark, new Vector3(SpawnPosition.x + x_Offset, 8f, SpawnPosition.z + z_Offset), Quaternion.identity);
                break;
            case Enemy_Types.Bulldozer:
                // Bulldozer enemy type
                Instantiate(Bulldozer, new Vector3(SpawnPosition.x + x_Offset, 8f, SpawnPosition.z + z_Offset), Quaternion.identity);
                break;
            case Enemy_Types.Medic:
                // Medic enemy type
                Instantiate(Medic, new Vector3(SpawnPosition.x + x_Offset, 8f, SpawnPosition.z + z_Offset), Quaternion.identity);
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
