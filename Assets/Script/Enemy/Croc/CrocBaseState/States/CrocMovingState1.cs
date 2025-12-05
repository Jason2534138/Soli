using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrocMovingState : CrocBaseState
{
    private float _timer;
    public CrocMovingState(CrocSM stateMachine) : base("CrocMovingState", stateMachine) { }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Moving");
        _animator.Play("Car_Moving");
        _timer = 6f;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        _timer -= Time.deltaTime;
        if (_sm._playerDetection.isSeeingPlayer) stateMachine.ChangeState(_sm.crocAggroState);
        if (_timer < 0f) stateMachine.ChangeState(_sm.crocIdleState);
        _rb.velocity = new Vector2(_sm._isFacingRight ? 3f : -3f, _rb.velocity.y);
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate(); 
    }
    public override void Exit()
    {
        base.Exit();
    }

}
