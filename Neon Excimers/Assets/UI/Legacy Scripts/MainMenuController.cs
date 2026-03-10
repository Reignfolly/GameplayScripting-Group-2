using UnityEngine;
using UnityEngine.UIElements;
public class MainMenuController : MonoBehaviour
{
    public VisualElement ui;

    public Button playButton;
    public Button optionsButton;
    public Button quitButton;
    
    private void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;

        playButton = ui.Q<Button>("PlayButton");
        optionsButton = ui.Q<Button>("OptionsButton");
        quitButton = ui.Q<Button>("QuitButton");

        playButton.clicked += OnPlayButtonClicked;
        optionsButton.clicked += OnOptionsButtonClicked;
        quitButton.clicked += OnQuitButtonClicked;
    }
    private void OnPlayButtonClicked()
    {
        gameObject.SetActive(false);
    }
    private void OnOptionsButtonClicked()
    {
        // Open the options menu
        Debug.Log("Options button clicked");
        //ui.Q<VisualElement>("OptionsMenu").style.display = DisplayStyle.Flex;
    }
    private void OnQuitButtonClicked()
    {
        // Quit the application
        Debug.Log("Quit button clicked");
        Application.Quit();
    }

}
