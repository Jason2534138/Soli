using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public bool isSeeingPlayer = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        Transform targetTransform = collision.transform;
        Vector2 dir = (targetTransform.position - this.transform.position).normalized;
        if(Physics2D.Raycast(this.transform.position, dir, Vector2.Distance(targetTransform.position, this.transform.position), 1 << 6) == false)
        {
            isSeeingPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isSeeingPlayer = false;
    }
}
