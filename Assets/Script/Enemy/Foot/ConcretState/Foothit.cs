using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Foothit : FootBasseState
{
    
    private float stunTimer;
    
    public Foothit(FootSM stateMachine) : base("Foothit", stateMachine) { }

    public override void Enter()
    {
        base.Enter();

        stunTimer = 0f;
        _sm._animator.Play("Foot_Hit");

    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        stunTimer += Time.deltaTime;
        if(stunTimer > 0.5f)
        {
            stateMachine.ChangeState(((FootSM)stateMachine).footAggro);
            
        }
            
    }

    public override void Exit()
    {
        base.Exit();
    }

}
