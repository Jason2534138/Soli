using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeAirState : BladeBaseState
{
    public BladeAirState(Blade stateMachine) : base("BladeAirState", stateMachine) {} 
    // Start is called before the first frame update
    public override void Enter()
    {
        base.Enter();
        ResetSpeed();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (_grounded)
        {
            stateMachine.ChangeState(_sm.bladeIdleState);
        }

    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    private void ResetSpeed()
    {
        _rb.velocity = Vector2.zero;
    }
}
