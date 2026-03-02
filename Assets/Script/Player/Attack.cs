using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

using UnityEngine.Events;

public class Attack : MonoBehaviour
{
    

    private Health health;
    [SerializeField] private int damage;
    [SerializeField] private int stun;
    [SerializeField] private Vector2 knockBack;
    public bool canBlock;
    public bool canAttack;

    public UnityEvent AttackConnect;
    
    //public bool isBlocked;
    

    private Transform playerTransform;
    private void Awake()
    {
        //isBlocked = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canAttack)
        {
            IDamageable _idamageable = collision.GetComponentInParent<IDamageable>();
            _idamageable?.OnHit(damage);
            AttackConnect.Invoke();
        }
        else if (canBlock)
        {
            Debug.Log("Block");
            IBlockable _blockable = collision.GetComponentInParent<IBlockable>();
            _blockable?.OnGotBlock();
        }
        
        
        
    }
}
