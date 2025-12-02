using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public Vector2 boxSize;
    public float castDistance;
    public Vector2 groundDetectOffset;
    public LayerMask groundLayer;

    public BoxCollider2D boxCollider;
    public bool IsWalled()
    {
        if (boxCollider.IsTouchingLayers(groundLayer))
        {
            return true;
        }
        else return false;

    }
    public bool IsGrounded()
    {
        if (Physics2D.BoxCast((Vector2)transform.position + groundDetectOffset, boxSize, 0, -transform.up, castDistance, groundLayer))
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
        Gizmos.DrawWireCube(transform.position + (Vector3)groundDetectOffset - transform.up * castDistance, boxSize);
        
    }

    internal bool IsTouchingWall()
    {
        throw new NotImplementedException();
    }
}
