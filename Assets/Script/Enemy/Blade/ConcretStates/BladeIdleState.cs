using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeIdle : BladeGrounded
{
    public BladeIdle( Blade stateMachine) : base("BladeIdle", stateMachine) {}
    public override void Enter()
    {
        base.Enter();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        HandleMovingLogic();
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        HandleMovingPhysics();
    }
    public override void Exit()
    {
        base.Exit();
    }

    #region ¤èªk
    private void HandleMovingLogic()
    {
        if (_isWalking)
        {
            if (_idleTimer != 0) _idleTimer = 0f;
            _animator.Play("Blade_run");
        }
        else
        {
            _idleTimer += Time.deltaTime;
            _animator.Play("Blade_idle");
            if (_idleTimer > _idleTime)
            {
                _isWalking = true;
            }
        }
    }
    private void HandleMovingPhysics()
    {
        if (_isWalking)
        {
            _rb.velocity = new Vector2(_speed * dir, _rb.velocity.y);
        }
        else _rb.velocity = new Vector2(0, _rb.velocity.y);
    }
    private void Stun()
    {
        stateMachine.ChangeState(_sm.bladeStunState);
    }
    #endregion
}
