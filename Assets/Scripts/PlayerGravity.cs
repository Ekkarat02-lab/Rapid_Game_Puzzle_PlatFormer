using UnityEngine;

public class PlayerGravity : SharedPlayerController
{
    [SerializeField] private Transform rayPoint;
    [SerializeField] private Transform grabPoint;
    [SerializeField] private float rayDistance;

    private GameObject grabObject;
    private int layerIndex;
    void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>(); // รับส่วนประกอบ Rigidbody2D
        layerIndex = LayerMask.NameToLayer("Interactable");
    }

    void Update()
    {
        float horizontalInput = 0f; // กำหนดค่าการเคลื่อนที่ในแนวนอน

        groundCheck();
        // รับค่าการกดปุ่มซ้ายหรือขวา
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalInput = -1f; // เคลื่อนที่ไปทางซ้าย
            rayDistance = Mathf.Abs(rayDistance) * -1;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            horizontalInput = 1f; // เคลื่อนที่ไปทางขวา
            rayDistance = Mathf.Abs(rayDistance);
        }

        Move(horizontalInput); // เรียกใช้ฟังก์ชันการเคลื่อนที่

        // ตรวจสอบการกดปุ่มกระโดดและใช้ไอเทม
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            Jump();    
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            KeyItemController.Instance.Update();
            Debug.Log("Get And Set Item");
            
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
        HandleGrabOrDrop();
    }
    private void HandleGrabOrDrop()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(rayPoint.position, transform.right, rayDistance);
        if (grabObject == null)
        {
            if (hitInfo.collider != null && hitInfo.collider.gameObject.layer == layerIndex)
            {
                if (Input.GetKeyDown(KeyCode.DownArrow)) // Grab Object (DownArrow)
                {
                    grabObject = hitInfo.collider.gameObject;
                    Rigidbody2D objectRb = grabObject.GetComponent<Rigidbody2D>();
                    if (objectRb != null)
                    {
                        objectRb.isKinematic = true;
                        grabObject.transform.position = grabPoint.position;
                        grabObject.transform.SetParent(transform);
                    }
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.DownArrow)) // Drop Object (DownArrow)
            {
                Rigidbody2D objectRb = grabObject.GetComponent<Rigidbody2D>();
                if (objectRb != null)
                {
                    objectRb.isKinematic = false;
                    grabObject.transform.SetParent(null);
                    grabObject = null;
                }
            }
        }

        Debug.DrawRay(rayPoint.position, transform.right * rayDistance);
    }
}