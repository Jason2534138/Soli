using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootNormalState : FootBasseState
{

    public FootNormalState(string name, FootSM stateMachine) : base(name, stateMachine)
    {
        
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
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
