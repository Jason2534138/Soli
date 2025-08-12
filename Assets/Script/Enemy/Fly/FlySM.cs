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
        _playerDetection = GetComponentInChildren<PlayerDetection>();
        _health = GetComponent<Health>();
        _rb = GetComponent<Rigidbody2D>();
        flyIdleState = new FlyIdleState(this);
        flyAggroState = new FlyAggroState(this);
        flyAttackState = new FlyAttackState(this);
        _playerDetection = GetComponentInChildren<PlayerDetection>();
        _animator = GetComponent<Animator>();
        _health.ZeroHealth += die;
    }
    protected override BaseState GetInitialState()
    {
        return flyIdleState;
    }
    private void die(object sender, EventArgs e)
    {
        Destroy(this.gameObject);
    }
}
