using UnityEngine;

public class WeightCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Weight"))
        {
            if (gameObject.CompareTag("Windows"))
            {
                Destroy(gameObject);
            }
        }
    }
}