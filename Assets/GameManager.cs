using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private float Gravity; // Decrease gravity
    [SerializeField] private float GravityScale;

    public GameObject playerPrefab1;  // Assign Player 1 prefab
    public GameObject playerPrefab2;  // Assign Player 2 prefab

    public Transform[] spawnPoints;  // Array of spawn points for players

    private GameObject player1Instance;
    private GameObject player2Instance;

    private SinglePlayer player1SinglePlayerScript;
    private SinglePlayer player2SinglePlayerScript;

    private GameObject currentControlledPlayer;  // To track which player is currently controlled
    
    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        int playerMode = MenuController.playerMode;  // Get the player mode from MenuController

        if (playerMode == 2)
        {
            // StartWithSwitchMode: spawn both Player 1 and Player 2
            player1Instance = Instantiate(playerPrefab1, spawnPoints[0].position, Quaternion.identity);
            player2Instance = Instantiate(playerPrefab2, spawnPoints[1].position, Quaternion.identity);

            // Add SinglePlayer script to both Player 1 and Player 2
            player1SinglePlayerScript = player1Instance.AddComponent<SinglePlayer>();
            player2SinglePlayerScript = player2Instance.AddComponent<SinglePlayer>();

            // Disable SinglePlayer script in Player 2, enable in Player 1
            player1SinglePlayerScript.enabled = true;
            player2SinglePlayerScript.enabled = false;

            // Remove PlayerMovement.cs and PlayerGravity.cs from both players
            RemoveMovementScripts(player1Instance);
            RemoveGravityScripts(player2Instance);

            // Start by controlling Player 1
            currentControlledPlayer = player1Instance;
        }
    }

    void Update()
    {
        if (MenuController.playerMode == 2 && Input.GetKeyDown(KeyCode.Space))
        {
            SwitchPlayerControl();  // Switch control between Player 1 and Player 2
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
    }

    // Remove PlayerMovement component from the specified GameObject
    void RemoveMovementScripts(GameObject player)
    {
        PlayerMovement movementScript = player.GetComponent<PlayerMovement>();
        if (movementScript != null)
        {
            Destroy(movementScript);  // Permanently remove the PlayerMovement script
        }
    }

    // Remove PlayerGravity component from the specified GameObject
    void RemoveGravityScripts(GameObject player)
    {
        PlayerGravity gravityScript = player.GetComponent<PlayerGravity>();
        if (gravityScript != null)
        {
            Destroy(gravityScript);  // Permanently remove the PlayerGravity script
        }
    }

    public void GravityUp()
    {
        float gravitys = Gravity;
        Physics2D.gravity = new Vector2(0, gravitys);
        Gravity -= Time.deltaTime * GravityScale;
    }

    public void GravityDown()
    {
        float gravitys = Gravity;
        Physics2D.gravity = new Vector2(0, gravitys);
        Gravity += Time.deltaTime * GravityScale;
        if(Gravity >= 0)
        {
            Gravity = 0.1f;
        }
    }
}