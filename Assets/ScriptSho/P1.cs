using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1 : MonoBehaviour
{
    private GameManager gameManager;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    { //ดึง game Manager มาใช้
        if (Input.GetKeyDown(KeyCode.Z))
        {
            gameManager.GravityUp();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            gameManager.GravityDown();
        }
    }
}
