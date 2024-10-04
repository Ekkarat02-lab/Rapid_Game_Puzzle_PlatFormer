using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public pressurePlate[] pressurePlates; //pressure for open the door
    private bool isUnlocked = false; //check if door is unlocked.

    public bool unlockDoor;
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
    private bool areAllPlatesOccupied() //pressureplate is pressed.
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
        if (unlockDoor == true)
        {
            isUnlocked = true;
            Debug.Log("Door is unlocked");
            GetComponent<BoxCollider2D>().enabled = false; //open door
        }
        else if (unlockDoor == false)
        {
            isUnlocked = false;
            Debug.Log("Door is unlocked");
            GetComponent<BoxCollider2D>().enabled = true; //open door
        }
    }
    private void CloseDoor()
    {
        if (unlockDoor == true)
        {
            isUnlocked = false;
            Debug.Log("Door is closed");
            GetComponent<BoxCollider2D>().enabled = true;
        }
        else if (unlockDoor == false)
        {
            isUnlocked = true;
            Debug.Log("Door is closed");
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
