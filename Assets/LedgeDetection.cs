using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class LedgeDetection : MonoBehaviour
{
    [SerializeField] public BoxCollider2D _boxCollider2D;
    [SerializeField] private CircleCollider2D _circleCollider2D;
    [SerializeField] private LayerMask _layerMask;
    public bool CanLedgeClimb()
    {
        if (!_boxCollider2D.IsTouchingLayers(_layerMask) && _circleCollider2D.IsTouchingLayers(_layerMask)) return true;
        else return false;
    }
    
}
