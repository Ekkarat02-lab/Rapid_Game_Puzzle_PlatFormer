using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private float Gravity;
    [SerializeField] private float GravityScale;

    public GameObject playerPrefab1;
    public GameObject playerPrefab2;

    public Transform[] spawnPoints;

    public RectTransform arrowUI;

    // เพิ่มตัวแปรสำหรับ Panel และปุ่ม
    public GameObject completionPanel; // Panel ที่จะโชว์เมื่อผู้เล่นเสร็จ
    public Button backToMenuButton; // ปุ่มกลับไปที่เมนู
    public Button backToMenuButton1;

    public Button nextLevelButton; // ปุ่มไปยังเลเวลถัดไป



    private GameObject player1Instance;
    private GameObject player2Instance;

    private SinglePlayer player1SinglePlayerScript;
    private SinglePlayer player2SinglePlayerScript;

    private GameObject currentControlledPlayer;
    private bool isArrowVisible = true;

    private int playerMode;
    
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        int playerMode = MainMenu.playerMode;

        if (playerMode == 1)
        {
            player1Instance = Instantiate(playerPrefab1, spawnPoints[0].position, Quaternion.identity);
            player2Instance = Instantiate(playerPrefab2, spawnPoints[1].position, Quaternion.identity);
        }
        else if (playerMode == 2)
        {
            player1Instance = Instantiate(playerPrefab1, spawnPoints[0].position, Quaternion.identity);
            player2Instance = Instantiate(playerPrefab2, spawnPoints[1].position, Quaternion.identity);

            RemoveUnnecessaryScripts(player1Instance);
            RemoveUnnecessaryScripts(player2Instance);

            player1SinglePlayerScript = player1Instance.AddComponent<SinglePlayer>();
            player2SinglePlayerScript = player2Instance.AddComponent<SinglePlayer>();

            player1SinglePlayerScript.enabled = true;
            player2SinglePlayerScript.enabled = false;

            currentControlledPlayer = player1Instance;

            StartCoroutine(ShowArrowForLimitedTime());
        }
       
        // ตั้งค่าให้ปุ่มถูกปิดในตอนเริ่มต้น
        completionPanel.SetActive(false);
        backToMenuButton.onClick.AddListener(BackToMenu);
        backToMenuButton1.onClick.AddListener(BackToMenu);
        nextLevelButton.onClick.AddListener(NextLevel);
    }

    void Update()
    {
        if (MainMenu.playerMode == 2)
        {
            UpdateArrowUIPosition();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SwitchPlayerControl();
            }

            CheckAndRespawnIfNeeded();
            CheckForCompletion(); // เพิ่มการเรียกฟังก์ชันนี้ที่นี่
        }
    }
    
    public void CheckForCompletion()
    {
        // เช็คว่า player ทั้งสองตัวมีแท็ก "Complete" หรือไม่
        if (player1Instance != null && player2Instance != null)
        {
            if (player1Instance.CompareTag("Complete") && player2Instance.CompareTag("Complete"))
            {
                ShowCompletionPanel();
            }
        }
    }
    
    void RemoveUnnecessaryScripts(GameObject player)
    {
        var movementScript = player.GetComponent<PlayerMovement>();
        var gravityScript = player.GetComponent<PlayerGravity>();

        if (movementScript != null)
        {
            Destroy(movementScript);
        }

        if (gravityScript != null)
        {
            Destroy(gravityScript);
        }
    }

    void SwitchPlayerControl()
    {
        if (currentControlledPlayer == player1Instance)
        {
            player1SinglePlayerScript.enabled = false;
            player2SinglePlayerScript.enabled = true;

            currentControlledPlayer = player2Instance;
        }
        else
        {
            player2SinglePlayerScript.enabled = false;
            player1SinglePlayerScript.enabled = true;

            currentControlledPlayer = player1Instance;
        }

        StartCoroutine(ShowArrowForLimitedTime());
    }

    IEnumerator ShowArrowForLimitedTime()
    {
        isArrowVisible = true;
        arrowUI.gameObject.SetActive(true);
        UpdateArrowUIPosition();

        yield return new WaitForSeconds(2f);

        isArrowVisible = false;
        arrowUI.gameObject.SetActive(false);
    }

    void UpdateArrowUIPosition()
    {
        if (currentControlledPlayer != null)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(currentControlledPlayer.transform.position);

            arrowUI.position = screenPos;

            arrowUI.gameObject.SetActive(isArrowVisible);
        }
    }

    void CheckAndRespawnIfNeeded()
    {
        if (player1Instance == null)
        {
            player1Instance = Instantiate(playerPrefab1, spawnPoints[0].position, Quaternion.identity);
            RemoveUnnecessaryScripts(player1Instance);
            player1SinglePlayerScript = player1Instance.AddComponent<SinglePlayer>();
            player1SinglePlayerScript.enabled = false;

            player2SinglePlayerScript.enabled = true;
            currentControlledPlayer = player2Instance;

            StartCoroutine(ShowArrowForLimitedTime());
        }

        if (player2Instance == null)
        {
            player2Instance = Instantiate(playerPrefab2, spawnPoints[1].position, Quaternion.identity);
            RemoveUnnecessaryScripts(player2Instance);
            player2SinglePlayerScript = player2Instance.AddComponent<SinglePlayer>();
            player2SinglePlayerScript.enabled = false;

            player1SinglePlayerScript.enabled = true;
            currentControlledPlayer = player1Instance;

            StartCoroutine(ShowArrowForLimitedTime());
        }
    }

    void ShowCompletionPanel()
    {
        completionPanel.SetActive(true); // แสดง panel เมื่อทั้งสองตัวเสร็จ
        Time.timeScale = 0; // หยุดเกมชั่วคราว
    }

    public void BackToMenu()
    {
        Time.timeScale = 1; // เริ่มเกมใหม่
        SceneManager.LoadScene("MainMenu"); // เปลี่ยนไปยังหน้าเมนู
    }

    public void NextLevel()
    {
        Time.timeScale = 1; // เริ่มเกมใหม่
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // ไปยังเลเวลถัดไป
    }

    public void GravityUp()
    {
        float gravitys = Gravity;
        Physics2D.gravity = new Vector2(0, gravitys);
        Gravity -= Time.deltaTime * GravityScale;
        if (Gravity > 0)
        {
            Gravity = -9.82f;
        }
    }

    public void GravityDown()
    {
        float gravitys = Gravity;
        Physics2D.gravity = new Vector2(0, gravitys);
        Gravity += Time.deltaTime * GravityScale;
        if (Gravity >= 0)
        {
            Gravity = -0.1f;
        }
    }
    
    
}