using UnityEngine;

public class SharedPlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public Rigidbody2D rb;
    public float smoothTime = 0.3f;
    public Transform rayPointG;
    public float rayDistanceG;
   

    protected bool isGrounded;
    protected Vector2 currentVelocity = Vector2.zero;
    private int groundLayerIndex;

    private bool facingRight = true;

    public Animator animator;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        groundLayerIndex = LayerMask.NameToLayer("groundLayer");
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D not found! Ensure it's attached to the GameObject.");
        }

        if (animator == null)
        {
            Debug.LogError("Animator not found! Ensure it's attached to the GameObject.");
        }
    }

    public void Move(float horizontalInput)
    {
        if (rb == null)
        {
            return;
        }

        if (animator == null)
        {
            return;
        }

        float targetVelocityX = horizontalInput * moveSpeed;
        float newVelocityX = Mathf.SmoothDamp(rb.velocity.x, targetVelocityX, ref currentVelocity.x, smoothTime);
        rb.velocity = new Vector2(newVelocityX, rb.velocity.y);

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

    public void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
            animator.SetBool("IsJumping", true);
        }
    }

    public void groundCheck()
    {
        if (rayPointG == null)
        {
            return; // Exit the method if rayPointG is null
        }

        RaycastHit2D hit = Physics2D.Raycast(rayPointG.position, Vector2.down, rayDistanceG);

        if (hit.collider != null)
        {
            Debug.Log("Raycast hit: " + hit.collider.name);  // Log what the ray hits
            if (hit.collider.gameObject.layer == groundLayerIndex)
            {
                isGrounded = true;
                animator.SetBool("IsJumping", false);
            }
            else
            {
                isGrounded = false;
                animator.SetBool("IsJumping", true);
            }
        }
        else
        {
            isGrounded = false;
            animator.SetBool("IsJumping", true);
        }
        Debug.DrawRay(rayPointG.position, Vector2.down * rayDistanceG, Color.red);
    }


    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("IsJumping", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Complete"))
        {
            // เพิ่มแท็ก "Complete" ให้กับผู้เล่นเมื่อชนกับ GameObject ที่มีแท็กนี้
            gameObject.tag = "Complete";
            GameManager.Instance.CheckForCompletion(); // เรียกฟังก์ชันใน GameManager
        }
    }
}
