using UnityEngine;

public class KeyItemController : MonoBehaviour
{
    public static KeyItemController Instance;
    // ตัวแปรเพื่อเก็บสถานะของ item key
    private bool hasKey = false;
    private bool nearKeyItem = false; // ใช้เพื่อตรวจสอบว่าอยู่ใกล้ key item หรือไม่
    private bool nearDoor = false; // ใช้เพื่อตรวจสอบว่าอยู่ใกล้ประตูหรือไม่
    private GameObject keyItemObject; // ตัวแปรเพื่อเก็บ reference ของ KeyItem
    private GameObject doorObject; // ตัวแปรเพื่อเก็บ reference ของ Door

    public void Awake()
    {
        Instance = this;
    }

    public void Update()
    {
        // ตรวจสอบการกดปุ่ม S เพื่อเก็บ item key
        if (Input.GetKeyDown(KeyCode.S))
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
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) 
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
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ตรวจสอบว่าเข้ามาใกล้ key item หรือ door
        if (collision.CompareTag("KeyItem"))
        {
            nearKeyItem = true;
            keyItemObject = collision.gameObject; // เก็บ reference ของ KeyItem
        }
        else if (collision.CompareTag("Door"))
        {
            nearDoor = true;
            doorObject = collision.gameObject; // เก็บ reference ของ Door
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // ตรวจสอบว่าออกจากการใกล้ key item หรือ door
        if (collision.CompareTag("KeyItem"))
        {
            nearKeyItem = false;
            keyItemObject = null; // รีเซ็ต reference
        }
        else if (collision.CompareTag("Door"))
        {
            nearDoor = false;
            doorObject = null; // รีเซ็ต reference
        }
    }

    private void UnlockDoor()
    {
        // ทำการปลดล็อกประตูที่นี่
        Debug.Log("The door has been unlocked!");
        
        // ลบ item key หลังจากใช้งาน
        hasKey = false;
        
        // ลบประตูจาก scene
        Destroy(doorObject); // ใช้ reference ที่เก็บไว้
    }
}
