using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteracableObject : MonoBehaviour
{
    protected virtual void Interact()
    {
        Debug.Log("Interact with" + gameObject.name);
    }
    public void TriggerInteract()
    {
        Interact();
    }
}
