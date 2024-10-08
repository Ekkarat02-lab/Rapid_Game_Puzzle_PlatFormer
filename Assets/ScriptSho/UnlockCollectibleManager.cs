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
    // �ѧ��ѹ�Ŵ��ͤ�ٻ
    public void UnlockImage(int index)
    {
        if (index >= 0 && index < imageUnlocked.Length)
        {
            imageUnlocked[index] = true;
        }
    }

    
    public bool IsImageUnlocked(int index)// �ѧ��ѹ��Ǩ�ͺ����ٻ�١�Ŵ��ͤ�������
    {
        if (index >= 0 && index < imageUnlocked.Length)
        {
            return imageUnlocked[index];
        }
        return false;
    }
}
