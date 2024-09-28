using UnityEngine;

public class SharedPlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public Rigidbody2D rb;
    public float smoothTime = 0.3f;

    protected bool isGrounded;
    protected Vector2 currentVelocity = Vector2.zero;

    private bool facingRight = true;

    public Animator animator;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Move(float horizontalInput)
    {
        float targetVelocityX = horizontalInput * moveSpeed;
        float newVelocityX = Mathf.SmoothDamp(rb.velocity.x, targetVelocityX, ref currentVelocity.x, smoothTime);
        rb.velocity = new Vector2(newVelocityX, rb.velocity.y);

        // ตรวจจับความเร็วในแกน X เพื่อเปลี่ยนค่าแอนิเมชัน
        animator.SetFloat("Speed", Mathf.Abs(newVelocityX));

        FlipCharacter(horizontalInput);
    }

    private void FlipCharacter(float horizontalInput)
    {
        if (horizontalInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (horizontalInput < 0 && facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    // ฟังก์ชันกระโดด จะกระโดดได้ถ้าอยู่บนพื้นเท่านั้น
    public void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
            animator.SetBool("IsJumping", true); // เล่นแอนิเมชันกระโดด
        }
    }

    // ฟังก์ชันเช็คว่า Player ชนกับพื้นดินที่มีแท็ก "Ground"
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("IsJumping", false); // หยุดแอนิเมชันกระโดดเมื่อกลับถึงพื้น
        }
    }

    // ฟังก์ชันเช็คเมื่อ Player หลุดออกจากพื้นดิน
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}