using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class FootSM : StateMachine
{
    [HideInInspector]
    public FootIdle footIdle;
    [HideInInspector]
    public FootAggroState footAggro;
    [HideInInspector]
    public Foothit footHit;
    [HideInInspector]
    public FootStun footStun;
    [HideInInspector]
    public FootDeath footDeath;
    [HideInInspector]
    public FootBlocked footBlock;

    public Health _health;
    
    

    public bool isFacingRight = false;

    public Animator _animator;

    public PlayerDetection _playerDetection;

    public Rigidbody2D rb;

    public Transform[] patrolPoints;

    public void Awake()
    {
        _playerDetection = GetComponentInChildren<PlayerDetection>();
        _health = GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();
        footIdle = new FootIdle(this);
        footAggro = new FootAggroState(this);
        footHit = new Foothit(this);
        footDeath = new FootDeath(this);
        footStun = new FootStun(this);
        footBlock = new FootBlocked(this);
        _animator = GetComponent<Animator>();
        _health.ZeroHealth += die;
    }

    protected override BaseState GetInitialState()
    {
        return footIdle;
    }
    private void die(object sender, EventArgs e)
    {
        Destroy(this.gameObject);
    }
}
