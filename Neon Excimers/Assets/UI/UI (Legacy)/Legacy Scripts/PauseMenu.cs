using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // A boolean to keep track of the game's paused state
    private bool isPaused = false;

    // Optional: Reference to a UI pause menu GameObject to display when paused
    public GameObject MenuUI;

    void Update()
    {
        // Check for an input, e.g., the Escape key, to toggle the pause state
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        // Set the time scale to 0 to stop time
        Time.timeScale = 0f;
        isPaused = true;
        // Show the pause menu UI
        if (MenuUI != null)
        {
            MenuUI.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        // Set time scale back to 1 to resume time
        Time.timeScale = 1f;
        isPaused = false;

        if (MenuUI != null)
        {
            MenuUI.SetActive(false);
        }
    }
}