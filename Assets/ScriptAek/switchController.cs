using UnityEngine;

public class SwitchController : MonoBehaviour
{
    public GameObject singlePlayerPrefab;
    public GameObject twoPlayerPrefab;
    private GameObject currentPlayerPrefab;
    private bool isSinglePlayer = true;

    void Start()
    {
        // เริ่มเกมโดยใช้ prefab ของ single player
        currentPlayerPrefab = Instantiate(singlePlayerPrefab, transform.position, Quaternion.identity);
    }

    void Update()
    {
        // ตรวจสอบว่าผู้เล่นกด space bar หรือไม่เพื่อสลับ prefab
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchPrefab();
        }
    }

    void SwitchPrefab()
    {
        // ลบ prefab ปัจจุบันก่อน
        // Destroy(currentPlayerPrefab);

        // สลับระหว่าง single player และ two player
        if (isSinglePlayer)
        {
            currentPlayerPrefab = Instantiate(twoPlayerPrefab, transform.position, Quaternion.identity);
            isSinglePlayer = false;
        }
        else
        {
            currentPlayerPrefab = Instantiate(singlePlayerPrefab, transform.position, Quaternion.identity);
            isSinglePlayer = true;
        }
    }
}