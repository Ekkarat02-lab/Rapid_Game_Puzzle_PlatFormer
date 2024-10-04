using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; // ใช้งาน UI
using UnityEngine.SceneManagement; // ใช้งานการจัดการฉาก
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Button[] levelButtons; // อาร์เรย์ของปุ่มเลเวล

    private void Start()
    {
        UpdateLevelButtons(); // อัพเดตสถานะของปุ่มเลเวลเมื่อเริ่ม
    }

    private void UpdateLevelButtons()
    {
        for (int i = 0; i < levelButtons.Length; i++) // วนลูปผ่านทุกปุ่มเลเวล
        {
            int levelID = i + 1; // ระบุหมายเลขเลเวล (เริ่มจาก 1)
            bool isCleared = PlayerPref.instance.IsLevelCleared(levelID); // ตรวจสอบว่าเลเวลถูกเคลียร์หรือไม่

            // เปิดหรือปิดปุ่มตามสถานะการเคลียร์เลเวล
            if (isCleared || levelID == 1 || PlayerPref.instance.IsLevelCleared(levelID - 1))
            {
                levelButtons[i].interactable = true; // ทำให้ปุ่มสามารถกดได้
                levelButtons[i].GetComponent<Image>().color = Color.white; // เปลี่ยนสีปุ่มเป็นขาว
            }
            else
            {
                levelButtons[i].interactable = false; // ทำให้ปุ่มไม่สามารถกดได้
                levelButtons[i].GetComponent<Image>().color = Color.gray; // เปลี่ยนสีปุ่มเป็นสีเทา
            }

            int levelIndex = levelID; // เก็บหมายเลขเลเวลสำหรับใช้ใน listener
            levelButtons[i].onClick.AddListener(() => LoadLevel(levelIndex)); // เพิ่ม listener เพื่อโหลดเลเวลเมื่อคลิกปุ่ม
        }
    }

    // ฟังก์ชันสำหรับโหลดเลเวลที่เลือก
    public void LoadLevel(int levelID)
    {
        string sceneName = "Level " + levelID; // ตั้งชื่อฉากตามเลเวล
        PlayerPref.instance.currentLevel = levelID; // บันทึกเลเวลปัจจุบัน
        SceneManager.LoadScene(sceneName); // โหลดฉากที่เลือก
    }

    // ฟังก์ชันสำหรับเคลียร์ข้อมูลเลเวล
    public void ClearSaveData()
    {
        PlayerPref.instance.ClearAllSavedLevels(); // เคลียร์ข้อมูลเลเวลทั้งหมด
        UpdateLevelButtons(); // อัพเดตสถานะปุ่มเลเวล
    }

    // ฟังก์ชันสำหรับทำเครื่องหมายเลเวลที่ผ่าน
    public void CompleteLevel(int levelID)
    {
        PlayerPref.instance.UnlockNextLevel(levelID); // ปลดล็อกเลเวลถัดไป
        UpdateLevelButtons(); // อัพเดตสถานะปุ่มเลเวล
    }
}
