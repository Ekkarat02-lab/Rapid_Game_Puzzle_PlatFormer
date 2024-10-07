using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalleryManager : MonoBehaviour
{
    public Image[] galleryImages; // ��ҧ�֧ Image � UI ����Ѻ�ʴ����ٻ�Ҿ
    public Sprite[] unlockedSprites; // ��ҧ�֧ Sprite �ͧ�ٻ�Ҿ���Ŵ��ͤ��

    private void Start()
    {
        
        for (int i = 0; i < galleryImages.Length; i++)
        {
            if (UnlockCollectibleManager.instance.IsImageUnlocked(i))
            {
                
                galleryImages[i].sprite = unlockedSprites[i];// ����¹�ٻ� Gallery ������ٻ���Ŵ��ͤ

            }
        }
    }
}
