using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private float Gravity_ = -9.8f;
    [SerializeField] private float _Gravity = 9.8f;

     void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
     void FixedUpdate()
    {
        
       
    }
    //����¹���ԡ������繢��
    public void GravityUp( )
    {   float gravitys = _Gravity;
        Physics2D.gravity = new Vector2 (0,gravitys );

    }
    //����¹���ԡ�������ŧ
    public void GravityDown (  )
    {
        float  gravitys = Gravity_;
        Physics2D.gravity = new Vector2 (0,gravitys );
    }









}
