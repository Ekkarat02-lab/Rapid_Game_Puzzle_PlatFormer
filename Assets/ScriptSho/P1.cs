using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class P1 : MonoBehaviour
{
    private GameManager gameManager;

    public bool isGameOver;
    public GameObject gameOverUI;
    [SerializeField] TextMeshProUGUI deadText;
    public int deadCount;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Z))
        {
            gameManager.GravityUp();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            gameManager.GravityDown();
        }*/
    }
    private void OnTriggerEnter2D(Collider2D targer)
    {
        if (targer.gameObject.CompareTag("KillPlayer"))
        {
            isGameOver = true;
            gameOverUI.SetActive(true);
            Die();
        }
    }
    private void Die()
    {
        deadCount++;
        deadText.text = deadCount.ToString();
       Destroy(gameObject);
    }
}
