using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDetector : MonoBehaviour
{
    public GameObject AttachObj;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AttachObj = collision.gameObject;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(AttachObj = collision.gameObject)
        {
            AttachObj = null;
        }
    }
}
