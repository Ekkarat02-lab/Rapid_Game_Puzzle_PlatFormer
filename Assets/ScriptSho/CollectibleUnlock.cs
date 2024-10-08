using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleUnlock : MonoBehaviour
{
    public int imageIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1") || collision.CompareTag("Player2"))
        {
            UnlockCollectibleManager.instance.UnlockImage(imageIndex);
            Destroy(gameObject);
        }
    }
}
