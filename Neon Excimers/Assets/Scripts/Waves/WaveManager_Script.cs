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

    // Enemy Formation Variables
    // Formations represent the total number of enemies that the player will
    // Face should they survive long enough
    const int Default_Formation_Size = 1000; // Battalions are about 1,000 strong
    public int Current_Formation_Size = Default_Formation_Size;


    // End Formation Variables
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

    public DifficultyLevel BattalionDiff;
    public DifficultyLevel RegimentDiff;
    public DifficultyLevel BrigadeDiff;
    public DifficultyLevel DivisionDiff;
    public DifficultyLevel ArmyDiff;


    [SerializeField] Difficulty_Levels CurrentDifficultyLevel;
    [SerializeField] DifficultyLevel SelectedDifficultyClassInformation; // Easy access to whatever difficulty class we're at right now

    void Start()
    {
        // Start_New_Wave();
        CreateDifficulties();
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

    public void Start_New_Game(Difficulty_Levels NewDifficultlyLevel)
    {
        currentWave = 0;
        SetNewDifficulty(NewDifficultlyLevel);

        Start_New_Wave();
    }

    public void SetNewDifficulty(Difficulty_Levels NewDifficultyLevel)
    {
        switch (NewDifficultyLevel)
        {
            case Difficulty_Levels.Battalion: // Easy && Quicker
                SelectedDifficultyClassInformation = BattalionDiff;
                break;
            case Difficulty_Levels.Regiment: // Easier
                SelectedDifficultyClassInformation = RegimentDiff;
                break;
            case Difficulty_Levels.Brigade: // Fairest
                SelectedDifficultyClassInformation = BrigadeDiff;
                break;
            case Difficulty_Levels.Division: // Difficult
                SelectedDifficultyClassInformation = DivisionDiff;
                break;
            case Difficulty_Levels.Army: // Most difficult && Longest
                SelectedDifficultyClassInformation = ArmyDiff;
                break;
        }
    }

    public void Start_New_Wave()
    {
        currentWave += 1;
        switch (CurrentDifficultyLevel)
        {
            case Difficulty_Levels.Battalion: // Easy && Quicker
                Current_EnemyReserve = (Starting_EnemyReserve * currentWave) / 3; // Faster Waves
                Current_MaxNumOfEnemiesAliveAtOnce = Starting_MaxNumOfEnemiesAliveAtOnce * currentWave;
                break;
            case Difficulty_Levels.Regiment: // Easier
                Current_EnemyReserve = (Starting_EnemyReserve * currentWave) / 2;
                Current_MaxNumOfEnemiesAliveAtOnce = Starting_MaxNumOfEnemiesAliveAtOnce * currentWave;
                break;
            case Difficulty_Levels.Brigade: // Fairest
                Current_EnemyReserve = Starting_EnemyReserve * currentWave;
                Current_MaxNumOfEnemiesAliveAtOnce = Starting_MaxNumOfEnemiesAliveAtOnce * currentWave;
                break;
            case Difficulty_Levels.Division: // Difficult
                Current_EnemyReserve = (Starting_EnemyReserve * currentWave) * 2;
                Current_MaxNumOfEnemiesAliveAtOnce = Starting_MaxNumOfEnemiesAliveAtOnce * currentWave;
                break;
            case Difficulty_Levels.Army: // Most difficult && Longest
                Current_EnemyReserve = (Starting_EnemyReserve * currentWave) * 3; // Much Longer Waves
                Current_MaxNumOfEnemiesAliveAtOnce = Starting_MaxNumOfEnemiesAliveAtOnce * currentWave;
                break;
        }
        if (Current_MaxNumOfEnemiesAliveAtOnce > 80)
        {
            // Test limit: don't spawn more than 80 at once to prevent lag.
            Current_MaxNumOfEnemiesAliveAtOnce = 80;
        }

        // The enemies that were just in reserve are being deployed to fight the player

        Current_Formation_Size -= Current_EnemyReserve;
        Debug.Log("NEW WAVE: " + currentWave);
        Debug.Log("MAX NUMBER OF ALIVE ENEMIES: " + Current_MaxNumOfEnemiesAliveAtOnce);
        Debug.Log("REMAINING ENEMIES IN FORMATION " + Current_Formation_Size);
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
        // Considers the current wave and the current difficulty level
        var AI_Manager_Script = this.gameObject.GetComponentInChildren<AI_GameManager>();
        Current_EnemyReserve -= 1;
        switch (CurrentDifficultyLevel)
        {
            case Difficulty_Levels.Battalion: // Easy && Quicker
                Current_Formation_Size = 1000;
                break;
            case Difficulty_Levels.Regiment: // Easier
                Current_Formation_Size = 3000;
                break;
            case Difficulty_Levels.Brigade: // Fairest
                Current_Formation_Size = 7500;
                break;
            case Difficulty_Levels.Division: // Difficult
                Current_Formation_Size = 15000;
                break;
            case Difficulty_Levels.Army: // Most difficult && Longest
                Current_Formation_Size = 100000;
                break;
        }
        // Debug.Log(Current_EnemyReserve);
        // Debug.Log("Spawning a new enemy for the wave!");
        AI_Manager_Script.Spawn_Enemy();
    }

    void EndThisWave()
    {
        // Whatever needs to happen when the wave ends
        Start_New_Wave();
    }

    public void CreateDifficulties()
    {
        BattalionDifficulty_Function();
        RegimentDifficulty_Function();
        BrigadeDifficulty_Function();
        DivisionDifficulty_Function();
        ArmyDifficulty_Function();
    }

    private void BattalionDifficulty_Function() // Easiest
    {
        DifficultyLevel BattalionDiff = new()
        {
            Name = "Battalion", // Name
            Description = "A relaxed and quick battle.", // Description
            DifficultyEnum = Difficulty_Levels.Battalion, // Enum
            BaseReserveAmount = 50, // Base Reserve per wave
            BaseFormationAmount = 1000, // Base Formation per game
            WaveToSpawnEliteUnits = 5, // Wave to start spawning elite units
            BaseChanceForEliteUnit = 5, // Base Chance for elite units (when they can spawn)
            MaxChanceForEliteUnit = 20, // Max Chance for elite units
            IncreaseChanceForElitePerWave = 1, // Chance increase per wave for elite units
            WaveToSpawnAdvancedUnits = 12, // Wave to begin spawning advanced units
            BaseChanceForAdvancedUnit = 1, // Base Chance for advanced units
            MaxChanceForAdvancedUnit = 8, // Max Chance for advanced units
            IncreaseChanceForAdvancedPerWave = 1, // Chance increase per wave for advanced units
            TimeBetweenSpawns = 10 // Time in seconds between each enemy being spawned
        };
    }

    private void RegimentDifficulty_Function()
    {
        DifficultyLevel RegimentDiff = new()
        {
            Name = "Regiment", // Name
            Description = "A light challenge", // Description
            DifficultyEnum = Difficulty_Levels.Regiment, // Enum
            BaseReserveAmount = 75, // Base Reserve per wave
            BaseFormationAmount = 3000, // Base Formation per game
            WaveToSpawnEliteUnits = 4, // Wave to start spawning elite units
            BaseChanceForEliteUnit = 8, // Base Chance for elite units (when they can spawn)
            MaxChanceForEliteUnit = 20, // Max Chance for elite units
            IncreaseChanceForElitePerWave = 1, // Chance increase per wave for elite units
            WaveToSpawnAdvancedUnits = 12, // Wave to begin spawning advanced units
            BaseChanceForAdvancedUnit = 1, // Base Chance for advanced units
            MaxChanceForAdvancedUnit = 8, // Max Chance for advanced units
            IncreaseChanceForAdvancedPerWave = 1, // Chance increase per wave for advanced units
            TimeBetweenSpawns = 10 // Time in seconds between each enemy being spawned
        };
    }

    private void BrigadeDifficulty_Function() // Fairest
    {
        DifficultyLevel BrigadeDiff = new()
        {
            Name = "Brigade", // Name
            Description = "A fair fight.", // Description
            DifficultyEnum = Difficulty_Levels.Brigade, // Enum
            BaseReserveAmount = 100, // Base Reserve per wave
            BaseFormationAmount = 5500, // Base Formation per game
            WaveToSpawnEliteUnits = 4, // Wave to start spawning elite units
            BaseChanceForEliteUnit = 5, // Base Chance for elite units (when they can spawn)
            MaxChanceForEliteUnit = 25, // Max Chance for elite units
            IncreaseChanceForElitePerWave = 1, // Chance increase per wave for elite units
            WaveToSpawnAdvancedUnits = 12, // Wave to begin spawning advanced units
            BaseChanceForAdvancedUnit = 1, // Base Chance for advanced units
            MaxChanceForAdvancedUnit = 10, // Max Chance for advanced units
            IncreaseChanceForAdvancedPerWave = 1, // Chance increase per wave for advanced units
            TimeBetweenSpawns = 3 // Time in seconds between each enemy being spawned
        };
    }

    private void DivisionDifficulty_Function()
    {
        DifficultyLevel DivisionDiff = new()
        {
            Name = "Division", // Name
            Description = "A long and difficult fight.", // Description
            DifficultyEnum = Difficulty_Levels.Division, // Enum
            BaseReserveAmount = 300, // Base Reserve per wave
            BaseFormationAmount = 12000, // Base Formation per game
            WaveToSpawnEliteUnits = 2, // Wave to start spawning elite units
            BaseChanceForEliteUnit = 5, // Base Chance for elite units (when they can spawn)
            MaxChanceForEliteUnit = 30, // Max Chance for elite units
            IncreaseChanceForElitePerWave = 2, // Chance increase per wave for elite units
            WaveToSpawnAdvancedUnits = 12, // Wave to begin spawning advanced units
            BaseChanceForAdvancedUnit = 1, // Base Chance for advanced units
            MaxChanceForAdvancedUnit = 8, // Max Chance for advanced units
            IncreaseChanceForAdvancedPerWave = 1, // Chance increase per wave for advanced units
            TimeBetweenSpawns = 2// Time in seconds between each enemy being spawned
        };
    }

    private void ArmyDifficulty_Function() // Hardest
    {
        DifficultyLevel ArmyDiff = new()
        {
            Name = "Army", // Name
            Description = "You're going to lose.", // Description
            DifficultyEnum = Difficulty_Levels.Army, // Enum
            BaseReserveAmount = 500, // Base Reserve per wave
            BaseFormationAmount = 100000, // Base Formation per game
            WaveToSpawnEliteUnits = 1, // Wave to start spawning elite units
            BaseChanceForEliteUnit = 10, // Base Chance for elite units (when they can spawn)
            MaxChanceForEliteUnit = 35, // Max Chance for elite units
            IncreaseChanceForElitePerWave = 1, // Chance increase per wave for elite units
            WaveToSpawnAdvancedUnits = 3, // Wave to begin spawning advanced units
            BaseChanceForAdvancedUnit = 5, // Base Chance for advanced units
            MaxChanceForAdvancedUnit = 20, // Max Chance for advanced units
            IncreaseChanceForAdvancedPerWave = 1, // Chance increase per wave for advanced units
            TimeBetweenSpawns = 1 // Time in seconds between each enemy being spawned
        };
    }
}
