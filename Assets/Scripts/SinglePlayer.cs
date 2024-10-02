using UnityEngine;

public class SinglePlayer : SharedPlayerController
{
    void Start()
    {
        base.Start();
    }

    void Update()
    {
        float horizontalInput = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            horizontalInput = -1f; 
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = 1f; 
        }

        Move(horizontalInput);

        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            KeyItemController.Instance.Update();
            Debug.Log("Get And Set Item");
        }

        if (Input.GetMouseButton(0)) 
        {
            GameManager.Instance.GravityUp();
        }
        else if (Input.GetMouseButton(1)) 
        {
            GameManager.Instance.GravityDown();
        }

        MapRotation.Instance.Update();
    }
}
