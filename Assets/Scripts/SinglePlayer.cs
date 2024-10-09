using UnityEngine;

public class SinglePlayer : SharedPlayerController
{
    [SerializeField] private Transform rayPoint;
    [SerializeField] private Transform grabPoint;
    [SerializeField] private float rayDistance;
    [SerializeField] private float jumpForce = 5f; // เพิ่มค่า jumpForce สำหรับการกระโดด
    [SerializeField] private LayerMask groundLayer; // ระบุเลเยอร์ของพื้น
    [SerializeField] private Animator animator; // เพิ่ม Animator สำหรับอนิเมชัน

    private GameObject grabObject;
    private int layerIndex;
    private bool isGrounded = false;
    private Rigidbody2D rb;

    void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>(); // รับค่า Rigidbody2D ของตัวละคร
        animator = GetComponent<Animator>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D is not attached to the player!");
        }

        layerIndex = LayerMask.NameToLayer("Interactable");

        if (rayPoint == null)
        {
            Debug.LogError("rayPoint is not assigned!");
        }

        if (grabPoint == null)
        {
            Debug.LogError("grabPoint is not assigned!");
        }

        if (animator == null)
        {
            Debug.LogError("Animator is not assigned!");
        }
    }

    void Update()
    {
        float horizontalInput = 0f;

        // ตรวจสอบการเคลื่อนไหวซ้าย-ขวา
        if (Input.GetKey(KeyCode.A))
        {
            horizontalInput = -1f;
            rayDistance = Mathf.Abs(rayDistance) * -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = 1f;
            rayDistance = Mathf.Abs(rayDistance);
        }

        Move(horizontalInput);

        // กระโดดเมื่ออยู่บนพื้น
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            Jump();
        }

        // ตรวจสอบการกดปุ่มอื่นๆ
        if (Input.GetKeyDown(KeyCode.S))
        {
            KeyItemController.Instance.Update();
            Debug.Log("Get And Set Item");
        }

        if (Input.GetMouseButton(0)) 
        {
            GameManager.Instance.GravityUp();
        }
        else if (Input.GetMouseButton(1)) 
        {
            GameManager.Instance.GravityDown();
        }

        MapRotation.Instance.Update();
        HandleGrabOrDrop();
    }

    // ฟังก์ชัน Jump สำหรับการกระโดด
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce); // เพิ่มแรงกระโดด
        isGrounded = false; // เปลี่ยนสถานะหลังจากกระโดด
        animator.SetBool("IsJumping", true); // เล่นอนิเมชันการกระโดด
    }

    // ตรวจสอบการชนกับพื้น
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true; // ตั้งค่าว่าตัวละครอยู่บนพื้น
            animator.SetBool("IsJumping", false); // หยุดอนิเมชันการกระโดดเมื่อกลับสู่พื้น
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false; // ตัวละครออกจากพื้น
            animator.SetBool("IsJumping", true); // เล่นอนิเมชันการกระโดดขณะออกจากพื้น
        }
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
