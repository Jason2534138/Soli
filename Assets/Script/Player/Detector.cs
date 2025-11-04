using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    [Header("Ground Check")]
    public Vector2 boxSize = new Vector2(0.5f, 0.1f);
    public float castDistance = 0.1f;
    public LayerMask groundLayer;

    [Header("Wall Check")]
    public Vector2 wallBoxSize = new Vector2(0.2f, 1.0f); // ← 修正新增
    public float wallCastDistance = 0.1f;                 // ← 修正新增
    public LayerMask wallLayer;                           // ← 修正新增

    public bool IsGrounded()
    {
        return Physics2D.BoxCast(transform.position, boxSize, 0f, -transform.up, castDistance, groundLayer);
    }

    public bool IsTouchingWall()
    {
        float dir = transform.localScale.x > 0 ? 1f : -1f;
        Vector2 castDir = new Vector2(dir, 0f);
        RaycastHit2D hitWall = Physics2D.BoxCast(transform.position, wallBoxSize, 0f, castDir, wallCastDistance, wallLayer);
        return hitWall.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position - (Vector3)transform.up * castDistance, boxSize);

        Gizmos.color = Color.red;
        float dir = transform.localScale.x > 0 ? 1f : -1f;
        Gizmos.DrawWireCube(transform.position + Vector3.right * dir * wallCastDistance, wallBoxSize);
    }
}
