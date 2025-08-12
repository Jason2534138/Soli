using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootAggroState : FootBasseState
{
    private float aggroTimeMax = 5f;
    private float aggroTimer;
    private GameObject target;

    private Attack _attack;

    public FootAggroState(FootSM stateMachine) : base("FootAggroState", stateMachine) { }
    public override void Enter()
    {
        base.Enter();
        _attack = _sm.GetComponentInChildren<Attack>();
        
        aggroTimer = aggroTimeMax;
        target = GameObject.FindGameObjectWithTag("Player");
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        _sm._animator.Play("Foot_moving");
        if(_attack != null)
        {
            if (_attack.isBlocked)
            {
                stateMachine.ChangeState(_sm.footBlock);
            }
        }
       
        if (_sm._playerDetection.isSeeingPlayer)
        {
            aggroTimer = aggroTimeMax;
        }
        else
        {
            aggroTimer -= Time.deltaTime;
            if (aggroTimer < 0f) stateMachine.ChangeState(_sm.footIdle);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        int dir = target.transform.position.x > _sm.gameObject.transform.position.x ? 1 : -1;

        _rb.velocity = new Vector2(speed * dir, _rb.velocity.y);


    }
    public override void Exit()
    {
        base.Exit();
    }
    
}
