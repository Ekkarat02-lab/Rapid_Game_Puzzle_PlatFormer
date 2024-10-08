using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RespawnController : MonoBehaviour
{
    public static RespawnController Instance;


    public Transform respawnPointPlayer1;
    public Transform respawnPointPlayer2;
 
    private void Awake()
    {
        Instance = this;
        
    }
    private void Start()
    {
        
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1") || collision.CompareTag ("Player2"))
        {
            if (collision.gameObject.CompareTag("Player1"))
            {
                
                collision.transform.position = respawnPointPlayer1.position; // ส่ง Player 1 ไปที่ respawn point ของ Player 1
                
            }
            else if (collision.gameObject.CompareTag("Player2"))
            {
                
                collision.transform.position = respawnPointPlayer2.position; // ส่ง Player 2 ไปที่ respawn point ของ Player 2
               
            }

        }
    }
    public void SetRespawnPoint(Transform newRespawnPoint, string playerTag)
    {
        if (playerTag == "Player1")
        {
            respawnPointPlayer1 = newRespawnPoint; // ตั้งค่าจุดเกิดใหม่ของ Player 1
        }
        else if (playerTag == "Player2")
        {
            respawnPointPlayer2 = newRespawnPoint; // ตั้งค่าจุดเกิดใหม่ของ Player 2
        }
    }
    
    
}
