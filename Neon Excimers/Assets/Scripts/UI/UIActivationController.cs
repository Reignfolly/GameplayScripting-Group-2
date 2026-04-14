using UnityEngine;

public class UIActivationController : MonoBehaviour
{
    public GameObject UpgradeMenu;
    public GameObject PauseMenu;
    public GameObject StatMenu;

    public GameObject StartScreen;
    public GameObject DeathScreen;



    private bool isPaused = false;

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
    }

    public void ActivateUpgradeMenu()
    {
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
