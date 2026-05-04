using UnityEngine;

public class UIActivationController : MonoBehaviour
{   
    public PlayerStats PlayerStats;
    public GameObject UpgradeMenu;
    public GameObject PauseMenu;
    public GameObject StatMenu;

    public GameObject StartScreen;
    public GameObject DeathScreen;

    public UpgradeSelector upgradeSelector;



    private bool isPaused = false;

    public int upgradeThreshold = 10;
    public int ThresholdIncrement = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && DeathScreen.activeSelf == false && StartScreen.activeSelf == false)
        {
            TogglePauseMenu();
        }

        if (Input.GetKey(KeyCode.Tab))
        {
            StatMenu.SetActive(true);
        }
        else
        {
            StatMenu.SetActive(false);
        }

        if (PlayerStats.KillCount >= upgradeThreshold && UpgradeMenu.activeSelf == false && DeathScreen.activeSelf == false && StartScreen.activeSelf == false)
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
        PauseTime();
    }

    public void DeactivateUpgradeMenu()
    {
        UpgradeMenu.SetActive(false);
        UnpauseTime();
    }

    public void TogglePauseMenu()
    {
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
