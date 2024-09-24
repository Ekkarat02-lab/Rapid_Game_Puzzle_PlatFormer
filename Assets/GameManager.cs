using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [SerializeField] private float Gravity; // ค่าแรงโน้มถ่วงเริ่มต้น
    [SerializeField] private float GravityScale; // สเกลสำหรับการปรับแรงโน้มถ่วง

    public GameObject playerPrefab1;  // วัตถุ prefab ของ Player 1
    public GameObject playerPrefab2;  // วัตถุ prefab ของ Player 2

    public Transform[] spawnPoints;  // อาร์เรย์ของจุดเกิดสำหรับผู้เล่น

    public RectTransform arrowUI;  // UI ลูกศรเพื่อแสดงผู้เล่นที่ควบคุมอยู่

    private GameObject player1Instance; // อินสแตนซ์ของ Player 1
    private GameObject player2Instance; // อินสแตนซ์ของ Player 2

    private SinglePlayer player1SinglePlayerScript; // สคริปต์ SinglePlayer ของ Player 1
    private SinglePlayer player2SinglePlayerScript; // สคริปต์ SinglePlayer ของ Player 2

    private GameObject currentControlledPlayer;  // เพื่อติดตามผู้เล่นที่ควบคุมอยู่ในขณะนั้น
    
    void Awake()
    {
        Instance = this; // ตั้งค่า Instance ให้เป็นตัวเอง
    }
    
    void Start()
    {
        int playerMode = MenuController.playerMode;  // รับโหมดผู้เล่นจาก MenuController

        if (playerMode == 1) // กรณี StartWithPlayer1AndPlayer2
        {
            // สร้าง Player 1 และ Player 2 โดยไม่ต้องสลับสคริปต์
            player1Instance = Instantiate(playerPrefab1, spawnPoints[0].position, Quaternion.identity);
            player2Instance = Instantiate(playerPrefab2, spawnPoints[1].position, Quaternion.identity);
        }
        else if (playerMode == 2) // กรณี StartWithSwitchMode
        {
            // สร้าง Player 1 และ Player 2
            player1Instance = Instantiate(playerPrefab1, spawnPoints[0].position, Quaternion.identity);
            player2Instance = Instantiate(playerPrefab2, spawnPoints[1].position, Quaternion.identity);

            // ลบ PlayerMovement.cs และ PlayerGravity.cs จาก prefab1 และ prefab2
            RemoveUnnecessaryScripts(player1Instance);
            RemoveUnnecessaryScripts(player2Instance);

            // เพิ่มสคริปต์ SinglePlayer ให้กับทั้ง Player 1 และ Player 2
            player1SinglePlayerScript = player1Instance.AddComponent<SinglePlayer>();
            player2SinglePlayerScript = player2Instance.AddComponent<SinglePlayer>();

            // ปิดการใช้งาน SinglePlayer script ใน Player 2, เปิดใช้งานใน Player 1
            player1SinglePlayerScript.enabled = true;
            player2SinglePlayerScript.enabled = false;

            // เริ่มต้นด้วยการควบคุม Player 1
            currentControlledPlayer = player1Instance;

            // ปรับตำแหน่ง UI ลูกศรให้ชี้ไปที่ผู้เล่นที่ควบคุมอยู่ (Player 1)
            UpdateArrowUIPosition();
        }
    }

    void Update()
    {
        if (MenuController.playerMode == 2 && Input.GetKeyDown(KeyCode.Space))
        {
            SwitchPlayerControl();  // สลับการควบคุมระหว่าง Player 1 และ Player 2
        }

        // ปรับตำแหน่งลูกศร UI หากอยู่ในโหมดสลับ
        if (MenuController.playerMode == 2)
        {
            UpdateArrowUIPosition();
        }
    }

    // ฟังก์ชันสำหรับลบ PlayerMovement.cs และ PlayerGravity.cs
    void RemoveUnnecessaryScripts(GameObject player)
    {
        var movementScript = player.GetComponent<PlayerMovement>();
        var gravityScript = player.GetComponent<PlayerGravity>();

        if (movementScript != null)
        {
            Destroy(movementScript);  // ลบสคริปต์ PlayerMovement
        }

        if (gravityScript != null)
        {
            Destroy(gravityScript);  // ลบสคริปต์ PlayerGravity
        }
    }

    void SwitchPlayerControl()
    {
        if (currentControlledPlayer == player1Instance)
        {
            // สลับการควบคุมไปที่ Player 2
            player1SinglePlayerScript.enabled = false;  // ปิดการใช้งานสคริปต์ SinglePlayer ใน Player 1
            player2SinglePlayerScript.enabled = true;   // เปิดการใช้งานสคริปต์ SinglePlayer ใน Player 2

            currentControlledPlayer = player2Instance;  // ตั้งค่าการควบคุมให้เป็น Player 2
        }
        else
        {
            // สลับการควบคุมไปที่ Player 1
            player2SinglePlayerScript.enabled = false;  // ปิดการใช้งานสคริปต์ SinglePlayer ใน Player 2
            player1SinglePlayerScript.enabled = true;   // เปิดการใช้งานสคริปต์ SinglePlayer ใน Player 1

            currentControlledPlayer = player1Instance;  // ตั้งค่าการควบคุมให้เป็น Player 1
        }
    }

    // ฟังก์ชันสำหรับปรับตำแหน่ง UI ลูกศรให้ชี้ไปที่ผู้เล่นที่ควบคุมอยู่
    void UpdateArrowUIPosition()
    {
        if (currentControlledPlayer != null)
        {
            // แปลงตำแหน่งโลกของผู้เล่นปัจจุบันเป็นตำแหน่งบนหน้าจอ
            Vector3 screenPos = Camera.main.WorldToScreenPoint(currentControlledPlayer.transform.position);

            // ปรับตำแหน่ง UI ลูกศรให้ตรงกับตำแหน่งผู้เล่น
            arrowUI.position = screenPos;
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