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
        int dir = _sm._isFacingRight ? 1 : -1;
        _timer -= Time.deltaTime;
        if (_sm._playerDetection.isSeeingPlayer) stateMachine.ChangeState(_sm.crocAggroState);
        if (_timer < 0f) stateMachine.ChangeState(_sm.crocIdleState);
        if (Physics2D.Raycast(_sm.transform.position, new Vector2(dir * 0.5f, -1), 6f, 1 << 6) != true || Physics2D.Raycast(_sm.transform.position, new Vector2(dir, 0), 1f, 1 << 6) == true)
        {

            Vector2 vel = _rb.velocity;
            vel.x *= -1;
            _rb.velocity = vel;
            Flip();

        }
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
