using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public BoxCollider2D trigger;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1"))
        {
            RespawnController.Instance.SetRespawnPoint(transform, collision.tag); //Overrides the player's spawn point.
            trigger.enabled = false;
        }

    }

}
