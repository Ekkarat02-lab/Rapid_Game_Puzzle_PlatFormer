using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravity : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public Rigidbody2D rb;
    private bool isGrounded;

    private Vector2 currentVelocity = Vector2.zero; // ค่าความเร็วปัจจุบันสำหรับ SmoothDamp
    public float smoothTime = 0.3f; // ระยะเวลาที่ใช้ในการเปลี่ยนค่าความเร็ว

    void Update()
    {
        // SmoothDamp การเคลื่อนที่ในแกน X
        float targetVelocityX = 0f;
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            targetVelocityX = -moveSpeed;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            targetVelocityX = moveSpeed;
        }

        // ใช้ SmoothDamp เพื่อให้การเปลี่ยนแปลงความเร็วราบรื่น
        float newVelocityX = Mathf.SmoothDamp(rb.velocity.x, targetVelocityX, ref currentVelocity.x, smoothTime);
        rb.velocity = new Vector2(newVelocityX, rb.velocity.y);

        // การกระโดด
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
        }
        
        // ใช้ไอเท็ม
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            UseItem();
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
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void UseItem()
    {
        Debug.Log("Use Item");
    }
}