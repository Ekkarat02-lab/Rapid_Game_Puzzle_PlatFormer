using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public LevelManager levelManager; // อ้างอิงไปยัง LevelManager
    public GameObject mainMenu; // เมนูหลัก
    public GameObject selectLevelMenu; // เมนูการเลือกเลเวล

    // ฟังก์ชันนี้จะถูกเรียกเมื่อผู้ใช้คลิกปุ่มเริ่มเกม
    public void StartGameButton()
    {
        mainMenu.SetActive(false); // ซ่อนเมนูหลัก
        selectLevelMenu.SetActive(true); // แสดงเมนูการเลือกเลเวล
    }

    // ฟังก์ชันนี้จะถูกเรียกเมื่อผู้ใช้คลิกปุ่มกลับ
    public void BackButton()
    {
        selectLevelMenu.SetActive(false); // ซ่อนเมนูการเลือกเลเวล
        mainMenu.SetActive(true); // แสดงเมนูหลักอีกครั้ง
    }

    // ฟังก์ชันนี้จะถูกเรียกเมื่อผู้ใช้คลิกปุ่มล้างข้อมูล
    public void ClearSaveButton()
    {
        levelManager.ClearSaveData(); // เรียกใช้ฟังก์ชันจาก LevelManager เพื่อเคลียร์ข้อมูลที่บันทึก
    }

    // ฟังก์ชันนี้จะถูกเรียกเมื่อผู้ใช้คลิกปุ่มออกจากเกม
    public void ExitButton()
    {
        Application.Quit(); // ออกจากเกม
    }

    // ฟังก์ชันนี้จะถูกเรียกเมื่อผู้ใช้คลิกปุ่มสำหรับโหมด Player 1 และ Player 2
    public void StartWithPlayer1AndPlayer2()
    {
        MainMenu.playerMode = 1; // ตั้งค่า playerMode เป็น 1 สำหรับ Player 1 และ Player 2
        levelManager.LoadLevel(1); // โหลดฉากเกมเลเวล 1
    }

    // ฟังก์ชันนี้จะถูกเรียกเมื่อผู้ใช้คลิกปุ่มสำหรับโหมดผู้เล่นเดี่ยว
    public void StartWithSwitchMode()
    {
        MainMenu.playerMode = 2; // ตั้งค่า playerMode เป็น 2 สำหรับโหมดผู้เล่นเดี่ยวที่สามารถเปลี่ยนตัวละครได้
        levelManager.LoadLevel(1); // โหลดฉากเกมเลเวล 1
    }
}