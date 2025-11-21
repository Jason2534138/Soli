using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerZone : MonoBehaviour
{
    public bool oneShot = false;
    private bool alreadyEntered = false;
    private bool alreadyExited = false;

    public string collisionTag;
    
    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerExit;

    private void OmTriggerEnter2D(Collider2D collision) 
    {
        if (alreadyEntered)
            return;

        if (!string.IsNullOrEmpty(collisionTag) && !collision.CompareTag(collisionTag))
            return;

        onTriggerEnter?.Invoke();

        if(oneShot)
            alreadyEntered = true;
    }
    private void OmTriggerExit2D(Collider2D collision)
    {
        if (alreadyEntered)
            return;

        if (!string.IsNullOrEmpty(collisionTag) && !collision.CompareTag(collisionTag))
            return;

        onTriggerExit?.Invoke();

        if (oneShot)
            alreadyEntered = true;
    }

}
