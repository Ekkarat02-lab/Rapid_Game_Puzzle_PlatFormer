using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
     // ตัวแปร Array เก็บชื่อ Scene ที่จะใช้งาน
    public string[] scenes;

    // ตัวแปรสำหรับเก็บชื่อ Scene ที่เลือก
    private string selectedScene;

    // ตัวแปรสำหรับจัดการปุ่มเลือกโหมด
    public GameObject modeSelectionPanel; // Panel ที่มีปุ่มเลือกโหมด
    public Button player1AndPlayer2Button; // ปุ่มสำหรับโหมด Player 1 และ 2
    public Button switchModeButton; // ปุ่มสำหรับโหมด Switch

    public static int playerMode = 1;  // Default to player mode 1

    void Start()
    {
        // ซ่อนปุ่มเลือกโหมดในตอนเริ่มต้น
        modeSelectionPanel.SetActive(false);

        // ตั้งค่าให้ปุ่มเลือกโหมดเรียกฟังก์ชันที่เกี่ยวข้อง
        player1AndPlayer2Button.onClick.AddListener(StartWithPlayer1AndPlayer2);
        switchModeButton.onClick.AddListener(StartWithSwitchMode);
    }

    // ฟังก์ชันเรียกใช้งาน Scene ตามที่เลือกในเมนูหลัก
    public void LoadScene(int sceneIndex)
    {
        // ตรวจสอบว่า sceneIndex อยู่ในช่วงของ Array หรือไม่
        if (sceneIndex >= 0 && sceneIndex < scenes.Length)
        {
            selectedScene = scenes[sceneIndex]; // เก็บชื่อ Scene ที่เลือก
            modeSelectionPanel.SetActive(true); // แสดงปุ่มให้เลือกโหมด
        }
        else
        {
            Debug.LogWarning("Scene index out of bounds!");
        }
    }

    // ฟังก์ชันสำหรับเริ่มเกมด้วยโหมด Player 1 และ Player 2
    public void StartWithPlayer1AndPlayer2()
    {
        playerMode = 1;  // Set playerMode to 1 (normal mode)
        LoadSelectedScene();  // โหลด Scene ที่ผู้เล่นเลือก
    }

    // ฟังก์ชันสำหรับเริ่มเกมด้วยโหมด Single Player Switching Mode
    public void StartWithSwitchMode()
    {
        playerMode = 2;  // Set playerMode to 2 (switch mode)
        LoadSelectedScene();  // โหลด Scene ที่ผู้เล่นเลือก
    }

    // ฟังก์ชันสำหรับโหลด Scene ที่ผู้เล่นเลือก
    private void LoadSelectedScene()
    {
        if (!string.IsNullOrEmpty(selectedScene))
        {
            SceneManager.LoadScene(selectedScene);  // โหลด Scene ที่ถูกเลือก
        }
        else
        {
            Debug.LogWarning("No scene selected!");
        }
    }

    // ฟังก์ชันสำหรับออกจากเกม
    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit();  // ออกจากเกม
    }
}