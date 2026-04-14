using UnityEngine;

public class GameFlowManager : MonoBehaviour
{   
    public GameObject StartScreen;
    public GameObject DeathScreen;
    void Awake()
    {
        Time.timeScale = 0f;
    
    }

    public void StartGame()
    {   
        StartScreen.SetActive(false);
        Time.timeScale = 1f;
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
