using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CrocAggroState : CrocBaseState
{
    private float _timer;
    private GameObject target;
    
    public CrocAggroState(CrocSM stateMachine) : base("CrocAggroState", stateMachine) { }
    public override void Enter()
    {
        base.Enter();
        _timer = 5f; 
        Debug.Log("aggro");
        if (!target) target = GameObject.FindGameObjectWithTag("Player");
        _animator.Play("Car_Moving");
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!_sm._playerDetection.isSeeingPlayer) _timer -= Time.deltaTime;
        else _timer = 5f;
        if (_timer < 0)
        {
            stateMachine.ChangeState(_sm.crocIdleState);
        }
        if ((_sm.transform.position.x - target.transform.position.x > 5f && _sm._isFacingRight) || (_sm.transform.position.x - target.transform.position.x < -5f && !_sm._isFacingRight)) Flip();
        _rb.velocity = new Vector2(_sm._isFacingRight ? 6f : -6f, _rb.velocity.y);
        if ((_sm._attackDetection_bite.hasAttackTarget || _sm._attackDetection_spike.hasAttackTarget  || _sm._attackDetection_charge.hasAttackTarget) && _sm.attackTimer <= 0f)
        {
            stateMachine.ChangeState(_sm.crocAttackState);
        }
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
