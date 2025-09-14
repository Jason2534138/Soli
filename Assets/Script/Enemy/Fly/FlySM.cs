using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlySM : StateMachine
{
    [HideInInspector]
    public FlyIdleState flyIdleState;
    [HideInInspector]
    public FlyAttackState flyAttackState;
    [HideInInspector]
    public FlyAggroState flyAggroState;


    public Transform[] patrolPoints;


    public PlayerDetection _playerDetection;
    public Rigidbody2D _rb;
    public Animator _animator;
    public bool isFacingRight = false;
    public Health _health;
    private void Awake()
    {
        SetUp();
    }
    protected override BaseState GetInitialState()
    {
        return flyIdleState;
    }
    private void SetUp()
    {
        _playerDetection = GetComponentInChildren<PlayerDetection>();
        _health = GetComponent<Health>();
        _health.SetUp();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _playerDetection = GetComponentInChildren<PlayerDetection>();
        flyIdleState = new FlyIdleState(this);
        flyAggroState = new FlyAggroState(this);
        flyAttackState = new FlyAttackState(this);
        _health.healthSystem.Die += die;
    }
    private void die()
    {
        Destroy(this.gameObject);
    }
}
