using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyIdleState : FlyBaseState
{
    public FlyIdleState(FlySM stateMachine) : base("FlyIdleState", stateMachine){}

    
    private int dir;
    private bool _isWalking = true;
    private float _speed = 5f;

    public override void Enter()
    {
        base.Enter();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(_sm._playerDetection.isSeeingPlayer) stateMachine.ChangeState(_sm.flyAggroState);
        ChangeDirection();
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
    private void ChangeDirection()
    {
        int dir;
        if (_isFacingRight) dir = 1;
        else dir = -1;
        //當到走不了的地方時轉向(牆壁)
        //因為此處指定地板圖層的方法是指出其所在的層數的位置(如第六個圖層)，所以要是有改變圖層順序可能會出錯會需要修改
        if (Physics2D.Raycast(_sm.transform.position, new Vector2(dir, -1), 5f, 1 << 6) != true || Physics2D.Raycast(_sm.transform.position, new Vector2(dir, 0), 4f, 1 << 6) == true)
        {

            Vector2 vel = _rb.velocity;
            vel.x *= -1;
            _rb.velocity = vel;
            Flip();

        }

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
        int dir;
        if (_isFacingRight) dir = 1;
        else dir = -1;
        if (_isWalking)
        {
            _rb.velocity = new Vector2(_speed * dir, _rb.velocity.y);
        }
        else _rb.velocity = new Vector2(0, _rb.velocity.y);
    }

}

