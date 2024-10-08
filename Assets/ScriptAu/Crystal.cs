using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    [SerializeField]private Rigidbody2D rb;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Platform"))
        {
            rb.isKinematic = true;
            transform.position = other.transform.position;
            transform.parent = other.transform;
            rb.velocity =Vector2.zero;
            rb.angularDrag = 0f;
        }
    }
}
