using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
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
    }
}
