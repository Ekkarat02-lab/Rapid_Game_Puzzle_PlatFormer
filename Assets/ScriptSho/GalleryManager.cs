using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalleryManager : MonoBehaviour
{
    public Image[] galleryImages; // อ้างถึง Image ใน UI สำหรับแสดงผลรูปภาพ
    public Sprite[] unlockedSprites; // อ้างถึง Sprite ของรูปภาพที่ปลดล็อคได้

    private void Start()
    {
        
        for (int i = 0; i < galleryImages.Length; i++)// ตรวจสอบทุกรูปภาพใน Gallery และอัปเดตตามสถานะการปลดล็อค
        {
            if (UnlockCollectibleManager.instance.IsImageUnlocked(i))
            {
                
                galleryImages[i].sprite = unlockedSprites[i];// เปลี่ยนรูปใน Gallery ให้เป็นรูปที่ปลดล็อค

            }
        }
    }
}
