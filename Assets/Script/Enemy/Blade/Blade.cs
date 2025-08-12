using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Burst.CompilerServices;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;

public class Blade : StateMachine
{
    [HideInInspector]
    public BladeGrounded bladeIdleState;
    [HideInInspector]
    public BladeAggroState bladeAggroState;
    [HideInInspector]
    public BladeAttackState bladeAttackState;
    [HideInInspector]
    public BladeStunState bladeStunState;

    [SerializeField]public Rigidbody2D Rigidbody;

    public float _speed = 10f;

    public Transform[] patrolPoints;

    public Animator _animator;

    public StateMachine _sm;

    public bool isFacingRight = false;



    public Health _health;
    private void Awake()
    {
        
        _health = GetComponent<Health>();
        bladeIdleState = new BladeIdle(this);
        bladeAggroState = new BladeAggroState(this);
        bladeAttackState = new BladeAttackState(this);
        bladeStunState = new BladeStunState(this);



        _health.HitStun += Hit;
        _animator = GetComponent<Animator>();
        _health.ZeroHealth += die;
        
    }
    private void die(object sender, EventArgs e)
    {
        Destroy(this.gameObject);
    }


    protected override BaseState GetInitialState()
    {
        return bladeIdleState;
    }
    private void Hit(object sender, EventArgs e) {}
}

