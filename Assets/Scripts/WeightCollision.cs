using UnityEngine;

public class WeightCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ตรวจสอบว่าแท็กของวัตถุที่ชนกันคือ "Weight"
        if (collision.gameObject.CompareTag("Weight"))
        {
            // ตรวจสอบว่าแท็กของวัตถุที่ชนเป็น "Windows"
            if (gameObject.CompareTag("Windows"))
            {
                // ลบวัตถุที่มีแท็ก "Windows"
                Destroy(gameObject);
            }
        }
    }
}