using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject playModePanel;     // Panel สำหรับการเลือกโหมดการเล่น
    public Button player1AndPlayer2Button;
    public Button switchModeButton;

    public Button[] sceneButtons;        // ปุ่มสำหรับเลือกฉาก (เช่น button1, button2, button3)
    private string selectedScene;        // เก็บชื่อ scene ที่เลือก

    public static int playerMode = 1;

    void Start()
    {
        // ซ่อน panel ของการเลือกโหมดการเล่นตอนเริ่มเกม
        playModePanel.SetActive(false);

        // กำหนด Listener ให้ปุ่มต่างๆ
        player1AndPlayer2Button.onClick.AddListener(StartWithPlayer1AndPlayer2);
        switchModeButton.onClick.AddListener(StartWithSwitchMode);

        // กำหนด Listener ให้ปุ่มเลือกฉากแต่ละปุ่ม
        foreach (Button sceneButton in sceneButtons)
        {
            sceneButton.onClick.AddListener(() => OnSceneButtonClicked(sceneButton.name));
        }
    }

    // ฟังก์ชันนี้จะถูกเรียกเมื่อกดปุ่มเลือกฉาก
    public void OnSceneButtonClicked(string sceneName)
    {
        selectedScene = sceneName; // เก็บชื่อ scene ที่เลือกจากปุ่ม

        if (!string.IsNullOrEmpty(selectedScene))
        {
            playModePanel.SetActive(true);  // แสดง panel ของการเลือกโหมดการเล่น
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