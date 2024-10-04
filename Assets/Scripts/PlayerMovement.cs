using UnityEngine;

public class PlayerMovement : SharedPlayerController
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

        // รับค่าการกดปุ่ม A หรือ D
        if (Input.GetKey(KeyCode.A))
        {
            horizontalInput = -1f; // เคลื่อนที่ไปทางซ้าย
            rayDistance = Mathf.Abs(rayDistance) * -1;
            
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = 1f; // เคลื่อนที่ไปทางขวา
            rayDistance = Mathf.Abs(rayDistance);
        }

        Move(horizontalInput); // เรียกใช้ฟังก์ชันการเคลื่อนที่

        // ตรวจสอบการกดปุ่มกระโดดและใช้ไอเทม
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            KeyItemController.Instance.Update();
            Debug.Log("Get And Set Item");
            
        }

        // เรียกใช้การหมุนแผนที่
        MapRotation.Instance.Update();
        HandleGrabOrDrop();
        
    }
    
    
    private void HandleGrabOrDrop()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(rayPoint.position, transform.right, rayDistance);
        
        
        if (grabObject == null)
        {
            if (hitInfo.collider != null && hitInfo.collider.gameObject.layer == layerIndex)
            {
                if (Input.GetKeyDown(KeyCode.S)) // Grab Object (S)
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
            if (Input.GetKeyDown(KeyCode.S)) // Drop Object (S)
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