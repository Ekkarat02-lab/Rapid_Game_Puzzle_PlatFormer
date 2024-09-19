using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float mass;
    [SerializeField] private float Gravity; // Decrease gravity
    [SerializeField] private float GravityScale;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        
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