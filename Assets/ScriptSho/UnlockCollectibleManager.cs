using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockCollectibleManager : MonoBehaviour
{
    public static UnlockCollectibleManager instance;
    public bool[] imageUnlocked;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            imageUnlocked = new bool[4]; 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UnlockImage(int index)
    {
        if (index >= 0 && index < imageUnlocked.Length)
        {
            imageUnlocked[index] = true;
        }
    }
    
    public bool IsImageUnlocked(int index)
    {
        if (index >= 0 && index < imageUnlocked.Length)
        {
            return imageUnlocked[index];
        }
        return false;
    }
}
