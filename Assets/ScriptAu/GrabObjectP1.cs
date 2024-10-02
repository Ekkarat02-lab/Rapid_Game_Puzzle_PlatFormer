using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObjectP1 : MonoBehaviour
{
    [SerializeField] private Transform rayPoint;
    [SerializeField] private Transform grabPoint;
    [SerializeField] private float rayDistance;

    private GameObject grabObject;
    private int layerIndex;

    private void Start()
    {
        layerIndex = LayerMask.NameToLayer("Interactable");
    }

    private void Update()
    {
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
