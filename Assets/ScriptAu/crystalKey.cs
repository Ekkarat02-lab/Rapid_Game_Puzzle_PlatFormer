using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crystalKey : InteracableObject
{
    private List<Transform> players;
    private int playerIndex = 0;
    private bool isFollowing = false;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    protected override void Interact()
    {
        base.Interact();
        if(!isFollowing)
        {
            GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
            players = new List<Transform>(playerObjects.Length);

            foreach(GameObject playerObject in playerObjects)
            {
                players.Add(playerObject.transform);
            }
            isFollowing = true;
            Debug.Log("Crystal key is now following Player " + (playerIndex + 1));
        }
        else
        {
            playerIndex = (playerIndex +1) % players.Count;
            Debug.Log("Crystal key is now following Player " + (playerIndex + 1));
        }
    }

    private void FixedUpdate()
    {
        if (isFollowing && players != null && players.Count > 0)
        {
            // Calculate target position and move the key
            Vector2 targetPosition = players[playerIndex].position;
            Vector2 currentPosition = rb.position;

            // Move the key towards the player using physics
            Vector2 direction = (targetPosition - currentPosition).normalized;
            rb.velocity = direction * 5f; // Adjust speed as needed
        }
    }
}
