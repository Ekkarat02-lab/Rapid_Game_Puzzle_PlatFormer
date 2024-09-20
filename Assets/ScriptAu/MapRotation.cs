using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRotation : MonoBehaviour
{
    public static MapRotation Instance;
    public float rotationSpeed = 100f; // Rotation speed
    private Rigidbody2D rb;
    private float totalRotation = 0f; // Total rotation applied
    private bool isRotating = false; // Is currently rotating

    public void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        // Check for input to start rotation
        if (Input.GetKeyDown(KeyCode.E) && !isRotating)
        {
            StartCoroutine(RotateMap(-90f)); // Rotate right 90 degrees
        }
        else if (Input.GetKeyDown(KeyCode.Q) && !isRotating)
        {
            StartCoroutine(RotateMap(90f)); // Rotate left 90 degrees
        }
    }

    private IEnumerator RotateMap(float targetAngle)
    {
        isRotating = true;
        float initialRotation = rb.rotation;
        float targetRotation = initialRotation + targetAngle;
        float rotationStep = rotationSpeed * Time.deltaTime;

        while (Mathf.Abs(rb.rotation - targetRotation) > rotationStep)
        {
            // Rotate towards the target angle
            rb.MoveRotation(Mathf.MoveTowardsAngle(rb.rotation, targetRotation, rotationStep));
            yield return null; // Wait for the next frame
        }

        // Snap to target rotation to avoid overshooting
        rb.MoveRotation(targetRotation);
        isRotating = false; // Reset rotating state
    }
}
