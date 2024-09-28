using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RespawnController : MonoBehaviour
{
    public static RespawnController Instance;
    
    public Transform respawnPoint; // The starting point of that level
    private int totalDestroyCount = 0;
    public Text destroyCountText;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        UpdateDestroyCountUI();
    }
    private void FixedUpdate()
    {
        UpdateDestroyCountUI();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            totalDestroyCount++;
            collision.transform.position = respawnPoint.position;
            
        }
    }
    void UpdateDestroyCountUI()
    {
        destroyCountText.text = "Dead : " + totalDestroyCount;
    }
}
