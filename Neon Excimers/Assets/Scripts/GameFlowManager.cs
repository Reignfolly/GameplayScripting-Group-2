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

    public void StartGame()
    {   
        WeaponStats.rangeModifier = 0f;
        WeaponStats.damageModifier = 0f;
        WeaponStats.areaModifier = 0f;
        PlayerStats.moveSpeedModifier = 0f;
        PlayerStats.accelerationModifier = 0f;
        PlayerStats.dashSpeedModifier = 0f;
        PlayerStats.dashDurationModifier = 0f;
        PlayerStats.dashCooldownModifier = 0f;
        PlayerStats.KillCount = 0;

        StartScreen.SetActive(false);
        Time.timeScale = 1f;
        var GameManagerHolder = GameObject.Find("GameManager");
        var Wave_Manager_Script = GameManagerHolder.gameObject.GetComponent<WaveManager_Script>();
        Wave_Manager_Script.Start_New_Wave();
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
