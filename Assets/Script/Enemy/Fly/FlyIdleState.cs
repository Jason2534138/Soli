using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyIdleState : FlyBaseState
{
    public FlyIdleState(FlySM stateMachine) : base("FlyIdleState", stateMachine){}
    private int dir = 1;
    private bool _isWalking = true;
    private float _speed = 5f;
    
    public override void Enter()
    {
        base.Enter();
        
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if ((_rb.velocity.x > 0.1f && !_sm.isFacingRight) || (_rb.velocity.x < 0.1f && _sm.isFacingRight))
        {
            Flip();
        }
        if (_sm._playerDetection.isSeeingPlayer) stateMachine.ChangeState(_sm.flyAggroState);
        if(Vector2.Distance(_sm.transform.position, _patrolPoints[_currentPatrolPoint].transform.position) < 0.1f) ChangeDirection();
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
        Debug.Log(_sm.isFacingRight);
    }
    private void ChangeDirection()
    {
        if (_currentPatrolPoint >= _patrolPoints.Length - 1) dir = -1;
        else if (_currentPatrolPoint < 1) dir = 1;
        _currentPatrolPoint += dir;
    }
    private void HandleMovingLogic()
    {
        if (_rb.velocity.x > -0.01f && _rb.velocity.x < 0.01f)
        {
            _animator.Play("Fly_idle");
        }
        else
        {
            _animator.Play("Fly_idle");
        }
    }
    private void HandleMovingPhysics()
    {
        Vector2 dir;
        dir = (_patrolPoints[_currentPatrolPoint].transform.position - _sm.transform.position).normalized;

        //當到走不了的地方時轉向(牆壁)
        //因為此處指定地板圖層的方法是指出其所在的層數的位置(如第六個圖層)，所以要是有改變圖層順序可能會出錯會需要修改
        _rb.velocity = dir * _speed;


    }

}

