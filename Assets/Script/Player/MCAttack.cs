using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class MCAttack : MonoBehaviour
{
    // ?äQù…?
    // ùÚ·øù…?
    // ?ëﬁ
    // é{â¡?ë‘
    // ê•î€îÌäiÍ}

    //public event EventHandler OnHit;

    private Health health;
    [SerializeField] private int damage;
    [SerializeField] private int stun;
    [SerializeField] private int charge;
    [SerializeField] private Vector2 knockBack;
    [SerializeField] private MP _mp;

    private Transform playerTransform;

    private void Awake()
    {
        _mp = GetComponentInParent<MP>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (playerTransform == null)playerTransform = GetComponentInParent<Transform>();
            Transform TargetTransform = collision.transform;

        int onRight = (playerTransform.position.x - TargetTransform.position.x) > 0f ? -1 : 1;

        health = collision.gameObject.GetComponentInParent<Health>();
            health.OnHit(damage, stun, knockBack, onRight);
        AddPower(charge);
        
    }
    private void AddPower(int amount)
    {
        _mp.playerMPSystem.MPUp(amount);
    }
}
