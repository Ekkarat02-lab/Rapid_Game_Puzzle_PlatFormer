using UnityEngine;

public class PlayerPref : MonoBehaviour
{
    public int currentLevel; // เลเวลปัจจุบัน
    public static PlayerPref instance; // ตัวแปร singleton

    private void Awake()
    {
        if (instance == null) // ตรวจสอบว่า instance ยังไม่มีอยู่
        {
            instance = this; // กำหนดค่า instance เป็นตัวเอง
            DontDestroyOnLoad(gameObject); // ทำให้ไม่ถูกทำลายเมื่อโหลดฉากใหม่
        }
        else
        {
            Destroy(gameObject); // ทำลาย instance ที่ซ้ำกัน
        }
    }

    // ฟังก์ชันสำหรับบันทึกเลเวลที่ผ่าน
    public void SaveClearedLevel(int levelID)
    {
        PlayerPrefs.SetInt("LevelCleared_" + levelID, 1); // บันทึกสถานะเลเวล
        Debug.Log("Level " + levelID + " has been cleared and saved."); // แสดงข้อความใน Console
    }

    // ฟังก์ชันสำหรับตรวจสอบว่าเลเวลถูกเคลียร์หรือไม่
    public bool IsLevelCleared(int levelID)
    {
        return PlayerPrefs.GetInt("LevelCleared_" + levelID, 0) == 1; // คืนค่า true/false
    }

    // ฟังก์ชันสำหรับเคลียร์ข้อมูลเลเวลทั้งหมด
    public void ClearAllSavedLevels()
    {
        PlayerPrefs.DeleteAll(); // ลบข้อมูลทั้งหมดใน PlayerPrefs
        Debug.Log("All saved levels have been cleared."); // แสดงข้อความใน Console
    }

    // ฟังก์ชันสำหรับปลดล็อกเลเวลถัดไป
    public void UnlockNextLevel(int currentLevelID)
    {
        SaveClearedLevel(currentLevelID); // บันทึกเลเวลที่ผ่าน
    }
}