using UnityEngine;

public class PlayerMovement : SharedPlayerController
{
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // รับส่วนประกอบ Rigidbody2D
    }

    void Update()
    {
        float horizontalInput = 0f; // กำหนดค่าการเคลื่อนที่ในแนวนอน

        // รับค่าการกดปุ่ม A หรือ D
        if (Input.GetKey(KeyCode.A))
        {
            horizontalInput = -1f; // เคลื่อนที่ไปทางซ้าย
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = 1f; // เคลื่อนที่ไปทางขวา
        }

        Move(horizontalInput); // เรียกใช้ฟังก์ชันการเคลื่อนที่

        // ตรวจสอบการกดปุ่มกระโดดและใช้ไอเทม
        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            KeyItemController.Instance.Update();
            Debug.Log("Get And Set Item");
            GrabObject.instance.Update();
            Debug.Log("Grab object");
        }

        // เรียกใช้การหมุนแผนที่
        MapRotation.Instance.Update();
    }
}