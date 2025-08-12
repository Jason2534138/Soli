using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeAirState : BaseState
{
    
    protected Blade _sm;
    private PlayerHealth _health;
    private Rigidbody2D _rb;
    private Transform _transform;
    
    private bool _grounded;

    private int _groundLayer = 1 << 6;
    public BladeAirState(Blade stateMachine) : base("BladeAirState", stateMachine) 
    {
        _sm = (Blade)stateMachine;
    } 
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
        _grounded = _sm.Rigidbody.velocity.y < Mathf.Epsilon && _sm.Rigidbody.IsTouchingLayers(_groundLayer);  
    }

}
