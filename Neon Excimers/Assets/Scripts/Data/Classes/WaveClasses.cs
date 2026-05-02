using UnityEngine;

public class WaveClasses : MonoBehaviour
{

}


public class DifficultyLevel
{
    public string Name = "Company"; // Name of the Difficulty Level 

    public string Description = "A quick test of your skills."; // Description for a menu

    public Difficulty_Levels DifficultyEnum = Difficulty_Levels.Company; // Enum associated with this Difficulty Level
    public int BaseReserveAmount = 20; // How Many units PER WAVE by default
    public int BaseFormationAmount = 200; // How many units PER GAME by default

    public int WaveToSpawnEliteUnits = 3; // Once we reach this wave number, start adding Elite Units into the wave
    public int BaseChanceForEliteUnit = 10; // chance in % a spawned unit will be elite (after reaching WaveToSpawnEliteUnits)
    public int MaxChanceForEliteUnit = 35; // Max chance in % for a spawned unti to be elite
    public int IncreaseChanceForElitePerWave = 1; // Increase the chance for elite units per wave

    public int WaveToSpawnAdvancedUnits = 6; // Once we reach this wave number, start adding Advanced Units into the wave
    public int BaseChanceForAdvancedUnit = 1; // chance in % a spawned unit will be Advanced (after reaching WaveToSpawnAdvancedUnits)
    public int MaxChanceForAdvancedUnit = 10; // Max chance in % for a spawned unti to be Advanced
    public int IncreaseChanceForAdvancedPerWave = 1; // Increase the chance for Advanced units per wave

    public float TimeBetweenSpawns = 5f; // The time the WaveManager will wait between spawns of individual enemies.
}