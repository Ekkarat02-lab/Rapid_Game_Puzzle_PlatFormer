using UnityEngine;

public class SwitchController : MonoBehaviour
{
    public GameObject singlePlayerPrefab; // Prefab สำหรับเล่นคนเดียว
    public GameObject twoPlayerPrefab; // Prefab สำหรับเล่นสองคน
    public GameObject threePlayerPrefab; // Prefab สำหรับเล่นสามคน

    private GameObject singlePlayerInstance; // Instance ของ single player
    private GameObject twoPlayerInstance; // Instance ของ two player
    private GameObject threePlayerInstance; // Instance ของ three player

    // ฟังก์ชันสำหรับสร้าง singlePlayerPrefab และ twoPlayerPrefab
    public void SpawnSingleAndTwoPlayer()
    {
        // ลบ instance ของ prefab ทั้งหมดก่อนสร้างใหม่ (ถ้ามี)
        DestroyExistingPrefabs();

        // สร้าง singlePlayerPrefab และ twoPlayerPrefab
        singlePlayerInstance = Instantiate(singlePlayerPrefab, transform.position, Quaternion.identity);
        twoPlayerInstance = Instantiate(twoPlayerPrefab, transform.position, Quaternion.identity);
    }

    // ฟังก์ชันสำหรับสร้าง threePlayerPrefab
    public void SpawnThreePlayer()
    {
        // ลบ instance ของ prefab ทั้งหมดก่อนสร้างใหม่ (ถ้ามี)
        DestroyExistingPrefabs();

        // สร้าง threePlayerPrefab
        threePlayerInstance = Instantiate(threePlayerPrefab, transform.position, Quaternion.identity);
    }

    // ฟังก์ชันสำหรับลบ instance ของ prefab ที่มีอยู่
    void DestroyExistingPrefabs()
    {
        if (singlePlayerInstance != null)
        {
            Destroy(singlePlayerInstance);
        }
        if (twoPlayerInstance != null)
        {
            Destroy(twoPlayerInstance);
        }
        if (threePlayerInstance != null)
        {
            Destroy(threePlayerInstance);
        }
    }
}