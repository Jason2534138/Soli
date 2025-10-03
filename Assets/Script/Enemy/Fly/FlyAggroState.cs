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
        
        base.LogicUpdate();
        if ((_sm.isFacingRight && _sm.transform.position.x > _target.transform.position.x) || (!_sm.isFacingRight && _sm.transform.position.x < _target.transform.position.x)) Flip();
        
        if (_sm._playerDetection.isSeeingPlayer)
        {
            aggroTimer = aggroTimeMax;
        }
        else
        {
            aggroTimer -= Time.deltaTime;
            if (aggroTimer < 0f) stateMachine.ChangeState(_sm.flyIdleState);
        }        
        if (attackCDTimer <= 0.1f)
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
        Vector2 targetPosOffset;
        
        
        if (_sm.transform.position.x > _target.transform.position.x)
        {
            dir = ((Vector2)_target.transform.position + _offset - this._rb.position + new Vector2(Random.Range(2f, -2f), Random.Range(2f, -2f))).normalized;
            if (Vector2.Distance(this._rb.position, (Vector2)_target.transform.position + _offset) > 3f) _rb.velocity = _speed * dir;
            else
            {
                if (attackCDTimer > 0f) attackCDTimer -= Time.deltaTime;
                //_rb.velocity = Vector2.zero;
            }
                
        }
        else
        {
            targetPosOffset = _offset;
            targetPosOffset.x *= -1;
            dir = ((Vector2)_target.transform.position + targetPosOffset - this._rb.position + new Vector2(Random.Range(2f, -2f), Random.Range(2f, -2f))).normalized;
            if (Vector2.Distance(this._rb.position, (Vector2)_target.transform.position + targetPosOffset) > 3f) _rb.velocity = _speed * dir;
            else
            {
                if (attackCDTimer > 0f) attackCDTimer -= Time.deltaTime;
                //_rb.velocity = Vector2.zero;
            }
        }
        
    }
}
