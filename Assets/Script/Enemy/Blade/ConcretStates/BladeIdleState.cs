using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeIdle : BladeGrounded
{
    
    private int dir;
    private bool _isWalking = true;

    private int currentTargetPoint;
    private float _idleTime = 3f;
    private float _idleTimer;

    private float speed = 5f;
    
    public BladeIdle( Blade stateMachine) : base("BladeIdle", stateMachine) {}
    // Start is called before the first frame update
    public override void Enter()
    {
        base.Enter();
        currentTargetPoint = 0;
        _sm._health.HitStun += Hit;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if (_playerDetection.isSeeingPlayer == true)
        {
            stateMachine.ChangeState(_sm.bladeAggroState);
            
        }
        if (_isWalking)
        {
            dir = _sm.patrolPoints[currentTargetPoint].position.x - _sm.transform.position.x > 0f ? 1 : -1;
            if (_idleTimer != 0) _idleTimer = 0f;
            _sm._animator.Play("Blade_run");
            if (Mathf.Abs(_sm.transform.position.x - _sm.patrolPoints[currentTargetPoint].position.x) < 0.1f)
            {
                _isWalking = false;


            }
        }
        else
        {
            _idleTimer += Time.deltaTime;
            _sm._animator.Play("Blade_idle");
            if (_idleTimer > _idleTime)
            {
                _isWalking = true;
                ChangePoint();
            }

        }

    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (_isWalking)
        {
            _rb.velocity = new Vector2(speed * dir, _rb.velocity.y);

        }
        else _rb.velocity = new Vector2(0, _rb.velocity.y);

    }
    public override void Exit()
    {
        base.Exit();
        _sm._health.HitStun -= Hit;
    }
    private void Stun(object sender, EventArgs e)
    {
        
        stateMachine.ChangeState(_sm.bladeStunState);
    }

    private void ChangePoint()
    {
        currentTargetPoint++;
        if (currentTargetPoint >= _sm.patrolPoints.Length) currentTargetPoint = 0;
    }
    private void Hit(object sender, EventArgs e)
    {
        stateMachine.ChangeState(_sm.bladeAggroState);
    }
}
