using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public pressurePlate[] pressurePlates; //pressure for open the door
    private bool isUnlocked = false; //check if door is unlocked.

    // Update is called once per frame
    void Update()
    {
        if(!isUnlocked && areAllPlatesOccupied())
        {
            UnlockedDoor();
        }
        if(isUnlocked && !areAllPlatesOccupied())
        {
            CloseDoor();
        }
    }
    private bool areAllPlatesOccupied()
    {
        foreach(pressurePlate plate in pressurePlates)
        {
            if(!plate.IsOccupied())
            {
                return false;
            }
        }
        return true;
    }

    private void UnlockedDoor()
    {
        isUnlocked = true;
        Debug.Log("Door is unlocked");
        GetComponent<BoxCollider2D>().enabled = false; //open door
    }
    private void CloseDoor()
    {
        isUnlocked = false;
        Debug.Log("Door is closed");
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
