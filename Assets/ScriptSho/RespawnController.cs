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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1") || collision.CompareTag ("Player2"))
        {
            if (collision.gameObject.CompareTag("Player1"))
            {
                collision.transform.position = respawnPointPlayer1.position; // �� Player 1 价�� respawn point �ͧ Player 1
            }
            else if (collision.gameObject.CompareTag("Player2"))
            {
                collision.transform.position = respawnPointPlayer2.position; // �� Player 2 价�� respawn point �ͧ Player 2
            }

        }
    }
    
    public void SetRespawnPoint(Transform newRespawnPoint, string playerTag)
    {
        if (playerTag == "Player1")
        {
            respawnPointPlayer1 = newRespawnPoint; // ��駤�Ҩش�Դ����ͧ Player 1
        }
        else if (playerTag == "Player2")
        {
            respawnPointPlayer2 = newRespawnPoint; // ��駤�Ҩش�Դ����ͧ Player 2
        }
    }
}
