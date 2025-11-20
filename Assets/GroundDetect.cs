using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetect : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))collision.gameObject.transform.parent = transform;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.transform.parent == transform) collision.gameObject.transform.parent = null;
    }

}
