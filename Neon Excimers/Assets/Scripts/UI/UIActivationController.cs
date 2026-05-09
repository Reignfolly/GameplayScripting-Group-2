using UnityEngine;

public class UIActivationController : MonoBehaviour
{   
    public PlayerStats PlayerStats;
    public GameObject UpgradeMenu;
    public GameObject PauseMenu;
    public GameObject StatMenu;
    public GameObject OptionsMenu;
    public GameObject StartScreen;
    public GameObject DeathScreen;

    public UpgradeSelector upgradeSelector;
    public StageInfo stageInfo;

    public StatDisplay statDisplay;

    public int KillCount = 0;
    private bool isPaused = false;
    public int upgradeThreshold = 10;
    public int ThresholdIncrement = 0;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && DeathScreen.activeSelf == false && StartScreen.activeSelf == false && OptionsMenu.activeSelf == false)
        {
            TogglePauseMenu();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && OptionsMenu.activeSelf == true)
        {
            ToggleOptionsMenu();
        }


        if (Input.GetKey(KeyCode.Tab))
        {   
            statDisplay.UpdateStatsDisplay();
            StatMenu.SetActive(true);
        }
        else
        {
            StatMenu.SetActive(false);
        }
        KillCount = PlayerStats.KillCount;
        if (KillCount >= upgradeThreshold && UpgradeMenu.activeSelf == false && DeathScreen.activeSelf == false && StartScreen.activeSelf == false)
        {
            ActivateUpgradeMenu();
            ThresholdIncrement += 2;
            upgradeThreshold += 10 + ThresholdIncrement;
        }


    }

    public void ActivateUpgradeMenu()
    {   
        if (upgradeSelector != null)
        {
            upgradeSelector.RandomizeUpgrades();
        }
        else
        {
            Debug.LogWarning("UpgradeSelector reference is missing on UIActivationController!");
        }
        UpgradeMenu.SetActive(true);
        stageInfo.IncrementLevelNumber();

        PauseTime();
    }

    public void DeactivateUpgradeMenu()
    {
        UpgradeMenu.SetActive(false);
        UnpauseTime();
    }

    public void ToggleOptionsMenu()
    {
        OptionsMenu.SetActive(!OptionsMenu.activeSelf);
    }

    public void TogglePauseMenu()
    {   
        if(OptionsMenu.activeSelf)
        {
            OptionsMenu.SetActive(false);
        }
        PauseMenu.SetActive(!PauseMenu.activeSelf);
        if (PauseMenu.activeSelf)
        {
            PauseTime();
        }
        else
        {
            UnpauseTime();
        }
    }

    public void PauseTime()
    {
        Time.timeScale = 0;
        isPaused = true;
    }

    public void UnpauseTime()
    {
        if (!UpgradeMenu.activeSelf && !PauseMenu.activeSelf)
        {
            Time.timeScale = 1;
            isPaused = false;
        }
    }
}
