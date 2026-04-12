using UnityEngine;

public class UIActivationController : MonoBehaviour
{
    public GameObject UpgradeMenu;
    public GameObject PauseMenu;
    public GameObject StatMenu;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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

    private void PauseTime()
    {
        Time.timeScale = 0;
        isPaused = true;
    }

    private void UnpauseTime()
    {
        if (!UpgradeMenu.activeSelf && !PauseMenu.activeSelf)
        {
            Time.timeScale = 1;
            isPaused = false;
        }
    }
}
