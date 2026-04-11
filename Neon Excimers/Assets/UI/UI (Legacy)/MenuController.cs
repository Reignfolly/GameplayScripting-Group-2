using UnityEngine;
using UnityEngine.UIElements;

public class MenuController : MonoBehaviour
{
    private VisualElement _ui;
    private Button _playButton;
    private Button _optionsButton;
    private Button _quitButton;

    private bool _isPaused = false;

  
    [SerializeField] private UIDocument uiDocument;

    private void Awake()
    {
        if (uiDocument == null)
            uiDocument = GetComponent<UIDocument>();

        _ui = uiDocument.rootVisualElement;

        // Initialize Buttons from UI Builder names
        _playButton = _ui.Q<Button>("PlayButton");
        _optionsButton = _ui.Q<Button>("OptionsButton");
        _quitButton = _ui.Q<Button>("QuitButton");

        // Register Click Events
        _playButton.clicked += ResumeGame; // "Play" resumes if already in-game
        _optionsButton.clicked += OnOptionsButtonClicked;
        _quitButton.clicked += OnQuitButtonClicked;


        PauseGame();
    }

    private void Update()
    {
        // Toggle pause with Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused) ResumeGame();
            else PauseGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        _isPaused = true;
        _ui.style.display = DisplayStyle.Flex; // Show Menu
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        _isPaused = false;
        _ui.style.display = DisplayStyle.None; // Hide Menu
    }

    private void OnOptionsButtonClicked()
    {
        Debug.Log("Options button clicked");
        // Logic for options menu goes here
    }

    private void OnQuitButtonClicked()
    {
        Debug.Log("Quit button clicked");
        Application.Quit();
    }
}