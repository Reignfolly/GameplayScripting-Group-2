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
    public float BaseChanceForEliteUnit = 10f; // chance in % a spawned unit will be elite (after reaching WaveToSpawnEliteUnits)
    public float MaxChanceForEliteUnit = 35f; // Max chance in % for a spawned unti to be elite
    public float IncreaseChanceForElitePerWave = 1f; // Increase the chance for elite units per wave

    public int WaveToSpawnAdvancedUnits = 6; // Once we reach this wave number, start adding Advanced Units into the wave
    public float BaseChanceForAdvancedUnit = 1f; // chance in % a spawned unit will be Advanced (after reaching WaveToSpawnAdvancedUnits)
    public float MaxChanceForAdvancedUnit = 10f; // Max chance in % for a spawned unti to be Advanced
    public float IncreaseChanceForAdvancedPerWave = 1f; // Increase the chance for Advanced units per wave

    public float TimeBetweenSpawns = 5f; // The time the WaveManager will wait between spawns of individual enemies.
}