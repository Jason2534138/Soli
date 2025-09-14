using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootAggroState : FootBasseState
{
    private float aggroTimeMax = 5f;
    private float aggroTimer;
    private GameObject target;

    private Attack _attack;

    public FootAggroState(FootSM stateMachine) : base("FootAggroState", stateMachine) { }
    public override void Enter()
    {
        base.Enter();
        SetUp();
        _sm._animator.Play("Foot_moving");
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        HandleDeaggro();
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        HandlePhysics();
    }
    public override void Exit()
    {
        base.Exit();
    }
    private void SetUp()
    {
        _attack = _sm.GetComponentInChildren<Attack>();

        aggroTimer = aggroTimeMax;
        target = GameObject.FindGameObjectWithTag("Player");
    }
    private void HandleDeaggro()
    {
        if (_sm._playerDetection.isSeeingPlayer)
        {
            aggroTimer = aggroTimeMax;
        }
        else
        {
            aggroTimer -= Time.deltaTime;
            if (aggroTimer < 0f) stateMachine.ChangeState(_sm.footIdle);
        }
    }
    private void HandlePhysics()
    {
        
        int dir;
        if ((target.transform.position.x > _sm.gameObject.transform.position.x && !_isFacingRight) || (target.transform.position.x < _sm.gameObject.transform.position.x && _isFacingRight)) Flip();
        if (_isFacingRight) dir = 1;
        else dir = -1;

        _rb.velocity = new Vector2(_speed * dir, _rb.velocity.y);

    }
}
