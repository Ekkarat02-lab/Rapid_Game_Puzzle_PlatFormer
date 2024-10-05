using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleUnlock : MonoBehaviour
{
    public int imageIndex; // �Ţ�ӴѺ�ͧ�ٻ���лŴ��ͤ

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            UnlockCollectibleManager.instance.UnlockImage(imageIndex);// �Ŵ��ͤ�ٻ�Ҿ�����¡�� UnlockCollectibleManager
            Destroy(gameObject); // ����� Object ��ѧ�ҡ��
        }
    }
}
