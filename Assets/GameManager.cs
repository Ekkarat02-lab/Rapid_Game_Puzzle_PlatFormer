using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float mass;
    [SerializeField] private float Gravity_UP; // Decrease gravity
    [SerializeField] private float Gravity_Down;  // Increase gravity
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
        float gravitys = Gravity_UP;
        Physics2D.gravity = new Vector2(0, gravitys);
    }

    public void GravityDown()
    {
        float gravitys = Gravity_Down;
        Physics2D.gravity = new Vector2(0, gravitys);
    }
}