using UnityEngine;

public class SinglePlayer : SharedPlayerController
{
    [SerializeField] private Transform rayPoint;
    [SerializeField] private Transform grabPoint;
    [SerializeField] private float rayDistance;

    private GameObject grabObject;
    private int layerIndex;
    private bool isGrounded = false;

    void Start()
    {
        base.Start();
        layerIndex = LayerMask.NameToLayer("Interactable");

        if (rayPoint == null)
        {
            Debug.LogError("rayPoint is not assigned!");
        }

        if (grabPoint == null)
        {
            Debug.LogError("grabPoint is not assigned!");
        }
    }

    void Update()
    {
        float horizontalInput = 0f;

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

        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            Jump();
        }

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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
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
