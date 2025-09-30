using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Attack : MonoBehaviour
{
    

    private Health health;
    [SerializeField] private int damage;
    [SerializeField] private int stun;
    [SerializeField] private Vector2 knockBack;
    
    
    public bool isBlocked;
    

    private Transform playerTransform;
    private void Awake()
    {
        isBlocked = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable _idamageable = collision.GetComponentInParent<IDamageable>();
        _idamageable.OnHit(damage);
        
    }
}
