using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeAirState : BladeBaseState
{
    
    
    
    
    private bool _grounded;

    private int _groundLayer = 1 << 6;
    public BladeAirState(Blade stateMachine) : base("BladeAirState", stateMachine) {} 
    // Start is called before the first frame update
    public override void Enter()
    {
        base.Enter();
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
        _grounded = _rb.velocity.y < Mathf.Epsilon && _rb.IsTouchingLayers(_groundLayer);  
    }
}
