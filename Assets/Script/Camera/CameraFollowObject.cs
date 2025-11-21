using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamearFollowObject : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _palyerTransform;

    [Header("Flip Scale Stats")]
    [SerializeField] private float _flipXScaleTime = 0.5f;

    private Coroutine _turnCoroutine;

    private PlayerMovement _player;

    private bool _isFacingRight;

    private void Awake()
    {

    }
}
    

