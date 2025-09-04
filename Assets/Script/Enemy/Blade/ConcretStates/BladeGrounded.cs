using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BladeGrounded : BladeBaseState
{
    public PlayerDetection _playerDetection;
    public BladeGrounded(string name, Blade stateMachine) : base(name, stateMachine){}
    public override void Enter()
    {
        base.Enter();
        _playerDetection = _sm.GetComponentInChildren<PlayerDetection>();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(_rb.velocity.y > 0.1f)
        {
        //StateMachine.ChangeState(_sm.bladeAirState);
        }
    }
}
