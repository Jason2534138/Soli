using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Versioning;
using UnityEngine;

public class BladeIdle : BladeBaseState
{
    public BladeIdle( Blade stateMachine) : base("BladeIdle", stateMachine) {}
    public override void Enter()
    {
        base.Enter();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        ChangeDirection();
        HandleMovingLogic();
        if (_playerDetection.isSeeingPlayer) stateMachine.ChangeState(_sm.bladeAggroState);
        if (!_grounded) stateMachine.ChangeState(_sm.bladeAirState);
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

    #region 功能
    private void HandleMovingLogic()
    {
        if (_rb.velocity.x > -0.01f && _rb.velocity.x < 0.01f)
        {
            _animator.Play("Blade_idle");
        }
        else
        {
            _animator.Play("Blade_run");
        }
    }
    private void HandleMovingPhysics()
    {
        int dir;
        if (_isFacingRight) dir = 1;
        else dir = -1;
        if (_isWalking)
        {
            _rb.velocity = new Vector2(_speed * dir, _rb.velocity.y);
        }
        else _rb.velocity = new Vector2(0, _rb.velocity.y);
    }
    private void ChangeDirection()
    {
        int dir;
        if(_isFacingRight)dir = 1;
        else dir = -1;
        //當到走不了的地方時轉向(牆壁)
        //因為此處指定地板圖層的方法是指出其所在的層數的位置(如第六個圖層)，所以要是有改變圖層順序可能會出錯會需要修改
        if(Physics2D.Raycast(_sm.transform.position, new Vector2(dir, -1), 4f, 1 << 6) != true || Physics2D.Raycast(_sm.transform.position, new Vector2(dir, 0), 4f, 1 << 6) == true)
        {
            
            Vector2 vel = _rb.velocity;
            vel.x *= -1;
            _rb.velocity = vel;
            Flip();
            
        }
        
    }
    private void Stun()
    {
        stateMachine.ChangeState(_sm.bladeStunState);
    }
    #endregion
}
