using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    public GameObject StartScreen;
    public GameObject DeathScreen;

    public PlayerStats PlayerStats;
    public WeaponStats WeaponStats;
    void Awake()
    {
        Time.timeScale = 0f;

    }

   
    public PlayerCharacterClass SelectedPlayerClass = new PlayerCharacterClass()
    {
        Name = "Basic", // Name
        Description = "A versatile, humanoid machine. Adaptable and reliable.", // Description
        lifeSteal = 1,
        baseMoveSpeed = 6f,
        baseAcceleration = 12f,
        baseDashSpeed = 14f,
        baseDashDuration = .6f,
        baseDashCooldown = 4f,
        baseRange = 50f,
        baseDamage = 50f,
        baseArea = 0.2f,
        baseAttackSpeed = 0.09f,
    };




    public void StartGame()
    {   
        ResetStatModifiers();
        SetStartingStats();
        WeaponStats.UpgradeRefresh();
        PlayerStats.UpgradeRefresh();

        StartScreen.SetActive(false);
        Time.timeScale = 1f;
        var GameManagerHolder = GameObject.Find("GameManager");
        var Wave_Manager_Script = GameManagerHolder.gameObject.GetComponent<WaveManager_Script>();
        Wave_Manager_Script.Start_New_Wave();
    }

    public void ResetStatModifiers()
    {
        WeaponStats.rangeModifier = 0f;
        WeaponStats.damageModifier = 0f;
        WeaponStats.areaModifier = 0f;
        WeaponStats.attackSpeedModifier = 0f;
        PlayerStats.moveSpeedModifier = 0f;
        PlayerStats.accelerationModifier = 0f;
        PlayerStats.dashSpeedModifier = 0f;
        PlayerStats.dashDurationModifier = 0f;
        PlayerStats.dashCooldownModifier = 0f;
        PlayerStats.KillCount = 0;
        PlayerStats.lifeSteal = 1;
    }

    public void SetStartingStats()
    {
        WeaponStats.baseArea = SelectedPlayerClass.baseArea;
        WeaponStats.baseAttackSpeed = SelectedPlayerClass.baseAttackSpeed;
        WeaponStats.baseDamage = SelectedPlayerClass.baseDamage;
        WeaponStats.baseRange = SelectedPlayerClass.baseRange;

        PlayerStats.baseAcceleration = SelectedPlayerClass.baseAcceleration;
        PlayerStats.baseDashCooldown = SelectedPlayerClass.baseDashCooldown;
        PlayerStats.baseDashDuration = SelectedPlayerClass.baseDashDuration;
        PlayerStats.baseDashSpeed = SelectedPlayerClass.baseDashSpeed;
        PlayerStats.baseMoveSpeed = SelectedPlayerClass.baseMoveSpeed;
    }

    public void SetNewPlayerCharacterClass(PlayerCharacterClass NewCharacterClass)
    {
        SelectedPlayerClass = NewCharacterClass;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver()
    {
        DeathScreen.SetActive(true);
        Time.timeScale = 0f;
    }

}
