using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalleryManager : MonoBehaviour
{
    public Image[] galleryImages; 
    public Sprite[] unlockedSprites; 

    private void Start()
    {
        for (int i = 0; i < galleryImages.Length; i++)
        {
            if (UnlockCollectibleManager.instance.IsImageUnlocked(i))
            {
                
                galleryImages[i].sprite = unlockedSprites[i];
            }
        }
    }
}
