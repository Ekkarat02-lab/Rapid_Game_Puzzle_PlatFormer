using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayer : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rigidbody2D;
    Vector2 vectorY;
    Vector2 vectorX;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) 
        { 
            vectorY = new Vector2(0, 10f);
            rigidbody2D.AddForce(vectorY, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rigidbody2D.velocity = new Vector2(-speed, 0);
            
        }
        if (Input.GetKey(KeyCode.D))
        {
            rigidbody2D.velocity = new Vector2(speed, 0);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            GetItem();
        }
        
        //Gravity Controller
        if (Input.GetMouseButton(0))
        {
            GameManager.Instance.GravityUp();
            Debug.Log(Physics2D.gravity);
        }
        else if (Input.GetMouseButton(1))
        {
            GameManager.Instance.GravityDown();
            Debug.Log(Physics2D.gravity);
        }
        
        //Map Controller
        MapRotation.Instance.Update();
    }

    void GetItem()
    {
        Debug.Log("Use and Get Item");
    }
}
