using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyIdleState : FlyBaseState
{
    public FlyIdleState(FlySM stateMachine) : base("FlyIdleState", stateMachine){}

    private int currentTargetPoint;
    private float _idleTime = 3f;
    private float _idleTimer;
    private int dir;
    private bool _isWalking = true;
    private float _speed = 5f;

    public override void Enter()
    {
        base.Enter();
        currentTargetPoint = 0;


    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(_sm._playerDetection.isSeeingPlayer) stateMachine.ChangeState(_sm.flyAggroState);
        Flip();
        if (_isWalking)
        {
            dir = _sm.patrolPoints[currentTargetPoint].position.x - _sm.transform.position.x > 0f ? 1 : -1;
            if (_idleTimer != 0) _idleTimer = 0f;
            _sm._animator.Play("Fly_idle");
            if (Mathf.Abs(_sm.transform.position.x - _sm.patrolPoints[currentTargetPoint].position.x) < 0.1f)
            {
                _isWalking = false;


            }
        }
        else
        {
            _idleTimer += Time.deltaTime;
            _sm._animator.Play("Fly_idle");
            if (_idleTimer > _idleTime)
            {
                _isWalking = true;
                ChangePoint();
            }

        }



        //if (_sm._playerDetection.isSeeingPlayer) stateMachine.ChangeState(_sm.);

    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (_isWalking)
        {
            _rb.velocity = new Vector2(_speed * dir, _rb.velocity.y);

        }
        else _rb.velocity = new Vector2(0, _rb.velocity.y);


    }
    public override void Exit()
    {
        base.Exit();
    }
    private void ChangePoint()
    {
        currentTargetPoint++;
        if (currentTargetPoint >= _sm.patrolPoints.Length) currentTargetPoint = 0;
    }
}
