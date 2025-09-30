using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FlyAggroState : FlyBaseState
{
    public FlyAggroState(FlySM stateMachine) : base("FlyAggroState", stateMachine){}

    private float aggroTimeMax = 5f;
    private float aggroTimer;
    private float attackCD = 5f;
    private float attackCDTimer;
    private GameObject _target;
    
    private float _speed = 10f;

    public override void Enter()
    {
        base.Enter();
        attackCDTimer = attackCD;
        _sm._animator.Play("Fly_idle");
        aggroTimer = aggroTimeMax;
        _target = GameObject.FindGameObjectWithTag("Player");
    }
    public override void LogicUpdate()
    {
        Debug.Log(Vector2.Distance(this._rb.position, (Vector2)_target.transform.position + _offset));
        base.LogicUpdate();
        if(attackCDTimer > 0f)attackCDTimer -= Time.deltaTime;
        if (_sm._playerDetection.isSeeingPlayer)
        {
            aggroTimer = aggroTimeMax;
        }
        else
        {
            aggroTimer -= Time.deltaTime;
            if (aggroTimer < 0f) stateMachine.ChangeState(_sm.flyIdleState);
        }        
        if (Vector2.Distance(this._rb.position + _offset, _target.transform.position) < 1f && attackCDTimer <= 0.1f)
        {
            
            stateMachine.ChangeState(_sm.flyAttackState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        HandlePhysics();
    }
    public override void Exit()
    {
        base.Exit();
    }
    private void HandlePhysics()
    {
        Vector2 dir;
        if ((_isFacingRight && this._rb.position.x < _target.transform.position.x) || (!_isFacingRight && this._rb.position.x < _target.transform.position.x)) Flip();
        dir = ((Vector2)_target.transform.position + _offset - this._rb.position).normalized;
        if(Vector2.Distance(this._rb.position, (Vector2)_target.transform.position + _offset) > 2f) _rb.velocity = _speed * dir;
        else _rb.velocity = Vector2.zero;
    }
}
