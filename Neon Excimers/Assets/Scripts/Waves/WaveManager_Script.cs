using UnityEngine;
using System;
using System.Collections.Generic;

public class WaveManager_Script : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int Current_Number_Of_Enemies = 0;
    const float Starting_Run_Update_Script_Interval = 5;
    [SerializeField] float Run_Update_Script_Interval = Starting_Run_Update_Script_Interval;
    [SerializeField] int currentWave = 0;
    const int Starting_EnemyReserve = 40;
    public int Current_EnemyReserve = Starting_EnemyReserve;

    const int Starting_MaxNumOfEnemiesAliveAtOnce = 12;
    public int Current_MaxNumOfEnemiesAliveAtOnce = Starting_MaxNumOfEnemiesAliveAtOnce;

    const float Starting_WaveSpawnCooldown = 20;
    public float Current_WaveSpawnCooldown = 0;


    const double Starting_EnemySpawnCooldown = 0.25;
    public double Current_EnemySpawnCooldown = 0;
    private bool WaitingToSpawnEnemies = false;
    private bool Currently_SpawningEnemies = false;





    void Start()
    {
        // Start_New_Wave();
    }

    // Update is called once per frame
    void Update()
    {
        Run_Update_Script_Interval -= Time.deltaTime;
        if (Run_Update_Script_Interval <= 0)
        {
            Run_Update_Script_Interval = Starting_Run_Update_Script_Interval;
            WaveUpdate();
        }

        /*if (WaitingToSpawnEnemies == true)
        {
            Current_WaveSpawnCooldown += Time.deltaTime;
            if (Current_WaveSpawnCooldown >= (Starting_WaveSpawnCooldown / currentWave))
            {
                Current_WaveSpawnCooldown = 0;
                SpawnWaveEnemies();
            }
        }*/

        if (Currently_SpawningEnemies == true && Current_EnemyReserve > 0)
        {
            Current_EnemySpawnCooldown += Time.deltaTime;
            if (Current_EnemySpawnCooldown >= Starting_EnemySpawnCooldown && Current_Number_Of_Enemies < Current_MaxNumOfEnemiesAliveAtOnce)
            {
                SpawnWaveEnemies();
            }
        }
    }

    public void Start_New_Wave()
    {
        currentWave += 1;
        Current_EnemyReserve = Starting_EnemyReserve * currentWave;
        Current_MaxNumOfEnemiesAliveAtOnce = Starting_MaxNumOfEnemiesAliveAtOnce * currentWave;
        if (Current_MaxNumOfEnemiesAliveAtOnce > 80)
        {
            // Test limit: don't spawn more than 80 at once to prevent lag.
            Current_MaxNumOfEnemiesAliveAtOnce = 80;
        }
        Debug.Log("NEW WAVE: " + currentWave);
        Debug.Log("MAX NUMBER OF ALIVE ENEMIES: " + Current_MaxNumOfEnemiesAliveAtOnce);
        SpawnWaveEnemies();
    }


    void WaveUpdate()
    {
        Debug.Log("Wave update.");

        var AI_Manager_Script = this.gameObject.GetComponentInChildren<AI_GameManager>();
        List<UnityEngine.GameObject> FullEnemyList = AI_Manager_Script.GiveMeEnemyList();

        if (Current_EnemyReserve <= 0)
        {
            if (Current_Number_Of_Enemies == 0)
            {
                Debug.Log("All enemies are dead, start a new wave.");
                EndThisWave();
            }
        }
        else
        {
            if (Current_Number_Of_Enemies < Current_MaxNumOfEnemiesAliveAtOnce && Current_EnemyReserve > 0)
            {
                // We should be trying to spawn enemies right now
                Debug.Log("Beginning to spawn enemies.");
                Currently_SpawningEnemies = true;
            }
        }
    }

    void SpawnWaveEnemies()
    {
        var AI_Manager_Script = this.gameObject.GetComponentInChildren<AI_GameManager>();

        if (Current_Number_Of_Enemies < Current_MaxNumOfEnemiesAliveAtOnce)
        {
            SpawnEnemyWithinWave();
        }

        if (Current_Number_Of_Enemies >= Current_MaxNumOfEnemiesAliveAtOnce || Current_EnemyReserve <= 0)
        {
            Debug.Log("Done spawning enemies.");
            // We are done spawning enemies for now
            WaitingToSpawnEnemies = false;
            Currently_SpawningEnemies = false;
        }

    }

    void SpawnEnemyWithinWave()
    {
        var AI_Manager_Script = this.gameObject.GetComponentInChildren<AI_GameManager>();
        Current_EnemyReserve -= 1;
        // Debug.Log(Current_EnemyReserve);
        Debug.Log("Spawning a new enemy for the wave!");
        AI_Manager_Script.Spawn_Enemy();
    }

    void EndThisWave()
    {
        // Whatever needs to happen when the wave ends
        Start_New_Wave();
    }
}
