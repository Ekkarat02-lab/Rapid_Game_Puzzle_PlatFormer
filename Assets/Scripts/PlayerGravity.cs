using UnityEngine;

public class PlayerGravity : SharedPlayerController
{
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // รับส่วนประกอบ Rigidbody2D
    }

    void Update()
    {
        float horizontalInput = 0f; // กำหนดค่าการเคลื่อนที่ในแนวนอน

        // รับค่าการกดปุ่มซ้ายหรือขวา
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalInput = -1f; // เคลื่อนที่ไปทางซ้าย
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            horizontalInput = 1f; // เคลื่อนที่ไปทางขวา
        }

        Move(horizontalInput); // เรียกใช้ฟังก์ชันการเคลื่อนที่

        // ตรวจสอบการกดปุ่มกระโดดและใช้ไอเทม
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            
        }

        // ปรับแรงโน้มถ่วงด้วยการกดปุ่มเมาส์
        if (Input.GetMouseButton(0)) // ปุ่มซ้าย
        {
            GameManager.Instance.GravityUp(); // เพิ่มแรงโน้มถ่วง
        }
        else if (Input.GetMouseButton(1)) // ปุ่มขวา
        {
            GameManager.Instance.GravityDown(); // ลดแรงโน้มถ่วง
        }
    }
}