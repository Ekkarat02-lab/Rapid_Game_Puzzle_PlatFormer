using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockCollectibleManager : MonoBehaviour
{
    public static UnlockCollectibleManager instance;
    public bool[] imageUnlocked;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        imageUnlocked = new bool[4]; 
    }
    // ¿Ñ§¡ìªÑ¹»Å´ÅçÍ¤ÃÙ»
    public void UnlockImage(int index)
    {
        if (index >= 0 && index < imageUnlocked.Length)
        {
            imageUnlocked[index] = true;
        }
    }

    
    public bool IsImageUnlocked(int index)// ¿Ñ§¡ìªÑ¹µÃÇ¨ÊÍºÇèÒÃÙ»¶Ù¡»Å´ÅçÍ¤ËÃ×ÍäÁè
    {
        if (index >= 0 && index < imageUnlocked.Length)
        {
            return imageUnlocked[index];
        }
        return false;
    }
}
