using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject playModePanel;    
    public Button player1AndPlayer2Button;
    public Button switchModeButton;

    public Button[] sceneButtons;
    private string selectedScene;        

    public static int playerMode = 1;

    void Start()
    {
        playModePanel.SetActive(false);
        
        player1AndPlayer2Button.onClick.AddListener(StartWithPlayer1AndPlayer2);
        switchModeButton.onClick.AddListener(StartWithSwitchMode);

        foreach (Button sceneButton in sceneButtons)
        {
            sceneButton.onClick.AddListener(() => OnSceneButtonClicked(sceneButton.name));
        }
    }

    public void OnSceneButtonClicked(string sceneName)
    {
        selectedScene = sceneName;

        if (!string.IsNullOrEmpty(selectedScene))
        {
            playModePanel.SetActive(true); 
        }
        else
        {
            Debug.LogWarning("No scene selected!");
        }
    }

    public void StartWithPlayer1AndPlayer2()
    {
        if (!string.IsNullOrEmpty(selectedScene))
        {
            playerMode = 1;
            LoadSelectedScene();
        }
        else
        {
            Debug.LogWarning("No scene selected! Please choose a scene before proceeding.");
        }
    }

    public void StartWithSwitchMode()
    {
        if (!string.IsNullOrEmpty(selectedScene))
        {
            playerMode = 2;
            LoadSelectedScene();
        }
        else
        {
            Debug.LogWarning("No scene selected! Please choose a scene before proceeding.");
        }
    }

    private void LoadSelectedScene()
    {
        if (!string.IsNullOrEmpty(selectedScene))
        {
            SceneManager.LoadScene(selectedScene);
        }
        else
        {
            Debug.LogWarning("No scene selected!");
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }
}