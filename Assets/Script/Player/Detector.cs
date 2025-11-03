using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;

    public Vector2 wallBoxSize;
    public float wallCastDistance;
    public bool IsWalled()
    {
        if(this.transform.localScale.x > 0)
        {
            if (Physics2D.BoxCast(transform.position, wallBoxSize, 0, transform.right, wallCastDistance, groundLayer))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (Physics2D.BoxCast(transform.position, wallBoxSize, 0, -transform.right, wallCastDistance, groundLayer))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
    }
    public bool IsGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer))
        {
            return true;
        }
        else 
        {
            return false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position-transform.up * castDistance, boxSize);
        Gizmos.DrawWireCube(transform.position+transform.right * wallCastDistance, wallBoxSize);
    }
}
