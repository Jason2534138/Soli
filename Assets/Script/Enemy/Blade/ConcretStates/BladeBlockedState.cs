using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeBlockedState : BaseState
{
    public BladeBlockedState(Blade stateMachine) : base("BladeBlockedState", stateMachine) { }
    // Start is called before the first frame update
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
