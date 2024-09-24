using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressurePlate : MonoBehaviour
{
    public string expectedTag = "Key"; //tag that is used for pressure plate
    private bool isOccupied = false; // 

    private void OnTriggerEnter2D(Collider2D other) //check if object is in and correct tag
    {
        if(other.CompareTag(expectedTag))
        {
            isOccupied = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)//Check if object is out
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
