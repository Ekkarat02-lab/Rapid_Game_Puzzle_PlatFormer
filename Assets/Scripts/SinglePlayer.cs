using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayer : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public Rigidbody2D rb;
    private bool isGrounded;

    private Vector2 currentVelocity = Vector2.zero; // ค่าความเร็วปัจจุบันสำหรับ SmoothDamp
    public float smoothTime = 0.3f; // ระยะเวลาที่ใช้ในการเปลี่ยนค่าความเร็ว

    private InteracableObject currentObj;
    public LayerMask Interactable;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // SmoothDamp การเคลื่อนที่ในแกน X
        float targetVelocityX = 0f;
        
        if (Input.GetKey(KeyCode.A))
        {
            targetVelocityX = -moveSpeed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            targetVelocityX = moveSpeed;
        }

        // ใช้ SmoothDamp เพื่อให้การเปลี่ยนแปลงความเร็วราบรื่น
        float newVelocityX = Mathf.SmoothDamp(rb.velocity.x, targetVelocityX, ref currentVelocity.x, smoothTime);
        rb.velocity = new Vector2(newVelocityX, rb.velocity.y);

        // การกระโดด
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
            Debug.Log(isGrounded);
        }
        
        // UseItem
        if (Input.GetKeyDown(KeyCode.S))
        {
            
            if (currentObj != null)
            {
                currentObj.TriggerInteract();
            }
            else
            {
                GetItem();
            }
        }
        
        // ปรับแรงโน้มถ่วงด้วยเมาส์
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }else if (collision.gameObject.CompareTag("KillPlayer"))
        {
            Destroy(gameObject);
        }
    }
    
    void GetItem()
    {
        Debug.Log("Use and Get Item");
       
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((Interactable & (1 << other.gameObject.layer)) != 0) // Check if the layer matches
        {
            currentObj = other.GetComponent<InteracableObject>(); // Get reference to the interactable object
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if ((Interactable & (1 << other.gameObject.layer)) != 0) // Check if the layer matches
        {
            currentObj = null; // Clear the reference when exiting the trigger
        }
    }
}