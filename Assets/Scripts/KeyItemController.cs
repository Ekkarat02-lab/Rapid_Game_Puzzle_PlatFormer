using UnityEngine;

public class KeyItemController : MonoBehaviour
{
    public static KeyItemController Instance;
    private bool hasKey = false;
    private bool nearKeyItem = false;
    private bool nearDoor = false;
    private bool nearPlatform = false; // เพิ่มตัวแปรสำหรับตรวจสอบแท่น
    private GameObject keyItemObject;
    private GameObject doorObject;
    private GameObject platformObject; // เก็บ reference ของ Platform

    public void Awake()
    {
        Instance = this;
    }

    public void Update()
    {
        // ตรวจสอบการกดปุ่ม S หรือ DownArrow เพื่อเก็บ item หรือใช้ item
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (nearKeyItem && !hasKey)
            {
                // เก็บ item key
                hasKey = true;
                Debug.Log("You have picked up the key!");

                // ลบ item key จาก scene
                Destroy(keyItemObject);
            }
            else if (nearDoor && hasKey)
            {
                // ใช้ item key เพื่อเปิดประตู
                UnlockDoor();
            }
            else if (nearPlatform && hasKey)
            {
                // วาง item บนแท่นและเปิดประตู
                PlaceItemOnPlatform();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ตรวจสอบว่าเข้ามาใกล้ key item, door หรือ platform
        if (collision.CompareTag("KeyItem"))
        {
            Debug.Log("KeyItem detected.");
            nearKeyItem = true;
            keyItemObject = collision.gameObject;
        }
        else if (collision.CompareTag("Door"))
        {
            Debug.Log("Door detected.");
            nearDoor = true;
            doorObject = collision.gameObject;
        }
        else if (collision.CompareTag("Platform"))
        {
            Debug.Log("Platform detected.");
            nearPlatform = true;
            platformObject = collision.gameObject; // เก็บ reference ของ Platform
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // ตรวจสอบว่าออกจากการใกล้ key item, door หรือ platform
        if (collision.CompareTag("KeyItem"))
        {
            nearKeyItem = false;
            keyItemObject = null;
        }
        else if (collision.CompareTag("Door"))
        {
            nearDoor = false;
            doorObject = null;
        }
        else if (collision.CompareTag("Platform"))
        {
            nearPlatform = false;
            platformObject = null;
        }
    }

    private void UnlockDoor()
    {
        Debug.Log("The door has been unlocked!");
        hasKey = false;

        // ลบประตูจาก scene
        if (doorObject != null)
        {
            Debug.Log("Destroying the door.");
            Destroy(doorObject);
        }
        else
        {
            Debug.Log("No door found to destroy.");
        }
    }

    private void PlaceItemOnPlatform()
    {
        Debug.Log("You placed the key on the platform!");
        hasKey = false;

        // ลบประตูทั้งหมดที่มี Tag "Door"
        GameObject[] doors = GameObject.FindGameObjectsWithTag("Door");
        if (doors.Length > 0)
        {
            foreach (GameObject door in doors)
            {
                Debug.Log("Destroying door: " + door.name);
                Destroy(door);
            }
        }
        else
        {
            Debug.Log("No doors found to destroy.");
        }
    }
}
