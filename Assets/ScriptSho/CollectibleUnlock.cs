using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleUnlock : MonoBehaviour
{
    public int imageIndex; // เลขลำดับของรูปที่จะปลดล็อค

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            UnlockCollectibleManager.instance.UnlockImage(imageIndex);// ปลดล็อครูปภาพโดยเรียกใช้ UnlockCollectibleManager
            Destroy(gameObject); // ทำลาย Object หลังจากชน
        }
    }
}
