using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAggroState : FlyBaseState
{
    public FlyAggroState(FlySM stateMachine) : base("FlyAggroState", stateMachine){}

    private float aggroTimeMax = 5f;
    private float aggroTimer;

    private GameObject _target;

    private int dir;
    
    private float _speed = 10f;

    public override void Enter()
    {
        base.Enter();
        Debug.Log("aggro");
        aggroTimer = aggroTimeMax;
        _target = GameObject.FindGameObjectWithTag("Player");
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Flip();
        dir = _target.transform.position.x - _sm.transform.position.x > 0f ? 1 : -1;
        _sm._animator.Play("Fly_idle");
        if (_sm._playerDetection.isSeeingPlayer)
        {
            aggroTimer = aggroTimeMax;
        }
        else
        {
            aggroTimer -= Time.deltaTime;
            if (aggroTimer < 0f) stateMachine.ChangeState(_sm.flyIdleState);
        }
        
        if ((Mathf.Abs(_sm.transform.position.x - _target.transform.position.x) < 15.5f && Mathf.Abs(_sm.transform.position.x - _target.transform.position.x) > 14.5f) && _target.transform.position.y < _sm.transform.position.y)
        {
            stateMachine.ChangeState(_sm.flyAttackState);
        }


    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (Mathf.Abs(_sm.transform.position.x - _target.transform.position.x) > 15.5f)
        {
            _rb.velocity = new Vector2(_speed * dir, _rb.velocity.y);
        }
        else if (Mathf.Abs(_sm.transform.position.x - _target.transform.position.x) < 14.5f)
        {
            _rb.velocity = new Vector2(_speed * dir * -1, _rb.velocity.y);
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
    
}
