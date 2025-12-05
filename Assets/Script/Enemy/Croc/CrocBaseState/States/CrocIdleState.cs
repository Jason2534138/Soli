using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrocIdleState : CrocBaseState
{
    private float _timer;
    public CrocIdleState(CrocSM stateMachine) : base("CrocIdleState", stateMachine) { }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("idle");
        _timer = 3f;
        _animator.Play("Car_idle");
        Vector2 vel;
        vel = _sm._rb.velocity;
        vel.x = 0;  
        _sm._rb.velocity = vel;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        _timer -= Time.deltaTime;
        if (_sm._playerDetection.isSeeingPlayer) stateMachine.ChangeState(_sm.crocAggroState);
        if (_timer < 0f)
        {
            int random;
            random = Random.Range(0, 2);
            if (random == 0)
            {
                Flip();
                stateMachine.ChangeState(_sm.crocMovingState);
            }
            else
            {
                stateMachine.ChangeState(_sm.crocMovingState);
            }
        } 
            
        
    }
}
