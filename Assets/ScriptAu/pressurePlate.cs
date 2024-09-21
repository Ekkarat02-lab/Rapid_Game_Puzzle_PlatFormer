using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressurePlate : MonoBehaviour
{
    public string expectedTag = "Key";
    private bool isOccupied = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag(expectedTag))
        {
            isOccupied = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag(expectedTag))
        {
            isOccupied = false;
        }
    }

    public bool IsOccupied()
    {
        return isOccupied;
    }
}
