using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [SerializeField] private float Gravity; // ค่าแรงโน้มถ่วงเริ่มต้น
    [SerializeField] private float GravityScale; // สเกลสำหรับการปรับแรงโน้มถ่วง

    public GameObject playerPrefab1;  // Assign Player 1 prefab
    public GameObject playerPrefab2;  // Assign Player 2 prefab

    public Transform[] spawnPoints;  // Array of spawn points for players

    public RectTransform arrowUI;  // UI arrow to show the currently controlled player
      

    private GameObject player1Instance;
    private GameObject player2Instance;

    private SinglePlayer player1SinglePlayerScript;
    private SinglePlayer player2SinglePlayerScript;

    private GameObject currentControlledPlayer;  // To track which player is currently controlled
    private bool isArrowVisible = true;  // Track the visibility of the arrow
 
    private int playerMode;  // Mode to determine if it's StartWithSwitchMode or StartWithPlayer1AndPlayer2

    void Awake()
    {
        Instance = this; // ตั้งค่า Instance ให้เป็นตัวเอง
    }

    void Start()
    {
        int playerMode = MenuController.playerMode;  // Get the player mode from MenuController

        if (playerMode == 1) // กรณี StartWithPlayer1AndPlayer2
        {
            // Spawn both Player 1 and Player 2 without script switching and without SinglePlayer.cs
            player1Instance = Instantiate(playerPrefab1, spawnPoints[0].position, Quaternion.identity);
            player2Instance = Instantiate(playerPrefab2, spawnPoints[1].position, Quaternion.identity);

            // ไม่ต้องเพิ่มสคริปต์ SinglePlayer.cs
        }
        else if (playerMode == 2) // กรณี StartWithSwitchMode
        {
            // StartWithSwitchMode: spawn both Player 1 and Player 2
            player1Instance = Instantiate(playerPrefab1, spawnPoints[0].position, Quaternion.identity);
            player2Instance = Instantiate(playerPrefab2, spawnPoints[1].position, Quaternion.identity);

            // ลบ PlayerMovement.cs และ PlayerGravity.cs จาก prefab1 และ prefab2
            RemoveUnnecessaryScripts(player1Instance);
            RemoveUnnecessaryScripts(player2Instance);

            // Add SinglePlayer script to both Player 1 and Player 2
            player1SinglePlayerScript = player1Instance.AddComponent<SinglePlayer>();
            player2SinglePlayerScript = player2Instance.AddComponent<SinglePlayer>();

            // Disable SinglePlayer script in Player 2, enable in Player 1
            player1SinglePlayerScript.enabled = true;
            player2SinglePlayerScript.enabled = false;

            // Start by controlling Player 1
            currentControlledPlayer = player1Instance;

            // Show the arrow UI for 2 seconds and then hide it
            StartCoroutine(ShowArrowForLimitedTime());
        }

        // Update UI Text with initial counts
        
    }

    void Update()
    {
        if (MenuController.playerMode == 2)
        {
            // Continuously update the arrow position to follow the current controlled player
            UpdateArrowUIPosition();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SwitchPlayerControl();  // Switch control between Player 1 and Player 2
            }

            // Check if either prefab is destroyed
            CheckAndRespawnIfNeeded();
        }
    }

    // Method to remove PlayerMovement.cs and PlayerGravity.cs
    void RemoveUnnecessaryScripts(GameObject player)
    {
        var movementScript = player.GetComponent<PlayerMovement>();
        var gravityScript = player.GetComponent<PlayerGravity>();

        if (movementScript != null)
        {
            Destroy(movementScript);  // Remove PlayerMovement script
        }

        if (gravityScript != null)
        {
            Destroy(gravityScript);  // Remove PlayerGravity script
        }
    }

    void SwitchPlayerControl()
    {
        if (currentControlledPlayer == player1Instance)
        {
            // Switch control to Player 2
            player1SinglePlayerScript.enabled = false;  // Disable SinglePlayer script in Player 1
            player2SinglePlayerScript.enabled = true;   // Enable SinglePlayer script in Player 2

            currentControlledPlayer = player2Instance;  // Set current control to Player 2
        }
        else
        {
            // Switch control to Player 1
            player2SinglePlayerScript.enabled = false;  // Disable SinglePlayer script in Player 2
            player1SinglePlayerScript.enabled = true;   // Enable SinglePlayer script in Player 1

            currentControlledPlayer = player1Instance;  // Set current control to Player 1
        }

        // Show the arrow UI for 2 seconds after switching control
        StartCoroutine(ShowArrowForLimitedTime());
    }

    // Coroutine to show the arrow for 2 seconds and then hide it (but keep it following the player)
    IEnumerator ShowArrowForLimitedTime()
    {
        // Make the arrow visible and position it at the current controlled player
        isArrowVisible = true;
        arrowUI.gameObject.SetActive(true);
        UpdateArrowUIPosition();

        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);

        // Hide the arrow UI, but continue tracking its position
        isArrowVisible = false;
        arrowUI.gameObject.SetActive(false);
    }

    // Method to update the arrow UI position to point to the currently controlled player
    void UpdateArrowUIPosition()
    {
        if (currentControlledPlayer != null)
        {
            // Convert the world position of the current player to the screen position
            Vector3 screenPos = Camera.main.WorldToScreenPoint(currentControlledPlayer.transform.position);

            // Update the UI arrow position to match the player's screen position
            arrowUI.position = screenPos;

            // Only show the arrow if it is meant to be visible
            arrowUI.gameObject.SetActive(isArrowVisible);
        }
    }

    // Method to check if a prefab is destroyed and respawn it if needed
    void CheckAndRespawnIfNeeded()
    {
        if (player1Instance == null)
        {
                      
            

            // Respawn Player 1 at the spawn point and switch control to Player 2
            player1Instance = Instantiate(playerPrefab1, spawnPoints[0].position, Quaternion.identity);
            RemoveUnnecessaryScripts(player1Instance);
            player1SinglePlayerScript = player1Instance.AddComponent<SinglePlayer>();
            player1SinglePlayerScript.enabled = false;  // Disable it initially

            // Switch control to Player 2
            player2SinglePlayerScript.enabled = true;
            currentControlledPlayer = player2Instance;

            // Show the arrow for Player 2
            StartCoroutine(ShowArrowForLimitedTime());
        }

        if (player2Instance == null)
        {
                     

            // Respawn Player 2 at the spawn point and switch control to Player 1
            player2Instance = Instantiate(playerPrefab2, spawnPoints[1].position, Quaternion.identity);
            RemoveUnnecessaryScripts(player2Instance);
            player2SinglePlayerScript = player2Instance.AddComponent<SinglePlayer>();
            player2SinglePlayerScript.enabled = false;  // Disable it initially

            // Switch control to Player 1
            player1SinglePlayerScript.enabled = true;
            currentControlledPlayer = player1Instance;

            // Show the arrow for Player 1
            StartCoroutine(ShowArrowForLimitedTime());
        }
    }

   
    

    public void GravityUp()
    {
        float gravitys = Gravity;
        Physics2D.gravity = new Vector2(0, gravitys);
        Gravity -= Time.deltaTime * GravityScale; // ลดค่าแรงโน้มถ่วง
    }

    public void GravityDown()
    {
        float gravitys = Gravity;
        Physics2D.gravity = new Vector2(0, gravitys);
        Gravity += Time.deltaTime * GravityScale; // เพิ่มค่าแรงโน้มถ่วง
        if(Gravity >= 0)
        {
            Gravity = 0.1f; // จำกัดแรงโน้มถ่วงต่ำสุด
        }
    }
}