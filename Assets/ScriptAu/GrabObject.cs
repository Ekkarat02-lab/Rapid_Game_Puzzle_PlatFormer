using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    public static GrabObject instance;
    [SerializeField]
    private Transform grabPoint;

    [SerializeField] 
    private Transform rayPoint;

    [SerializeField]
    private float rayDistance;

    private GameObject grabObject;
    private int layerIndex;
    // Start is called before the first frame update
    
    private void Start()
    {
        layerIndex = LayerMask.NameToLayer("Interactable");
        if(instance == null)
        {
            instance = this;
        }
            
    }

    // Update is called once per frame
    public void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(rayPoint.position, transform.right, rayDistance);
        if (grabObject == null) // If no object is currently grabbed
        {
            if (hitInfo.collider != null && hitInfo.collider.gameObject.layer == layerIndex)
            {
                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    grabObject = hitInfo.collider.gameObject;
                    Rigidbody2D rb = grabObject.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        rb.isKinematic = true; // Disable physics for grabbing
                        grabObject.transform.position = grabPoint.position;
                        grabObject.transform.SetParent(transform); // Attach to player
                    }
                }
            }
        }
        else // If an object is currently grabbed
        {
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                Rigidbody2D rb = grabObject.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.isKinematic = false; // Re-enable physics
                    grabObject.transform.SetParent(null); // Drop object
                    grabObject = null; // Clear grabbed object reference
                }
            }
        }
        Debug.DrawRay(rayPoint.position, transform.right * rayDistance);
    }
}
