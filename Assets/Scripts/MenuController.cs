using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public static int playerMode = 1;  // Default to player mode 1

    // Function to start the game with Player 1 and Player 2
    public void StartWithPlayer1AndPlayer2()
    {
        playerMode = 1;  // Set playerMode to 1 (normal mode)
        LoadPlayGameScene();  // Load the PlayGame scene
    }

    // Function to start the game with Single Player switching mode
    public void StartWithSwitchMode()
    {
        playerMode = 2;  // Set playerMode to 2 (switch mode)
        LoadPlayGameScene();  // Load the PlayGame scene
    }

    // Function to load the PlayGame scene
    private void LoadPlayGameScene()
    {
        SceneManager.LoadScene("PlayGame");
    }
}