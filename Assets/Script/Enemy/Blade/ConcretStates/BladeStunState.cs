using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeStunState : BladeBaseState
{
    
    private PlayerHealth _playerhealth;
    private float recoverTime;
    public BladeStunState(Blade stateMachine) : base("BladeStunState", stateMachine) {}
    // Start is called before the first frame update
    public override void Enter()
    {
        base.Enter();
        _playerhealth = _sm.gameObject.GetComponent<PlayerHealth>();
        _playerhealth.isStuned = true;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        recoverTime += Time.deltaTime;

    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public override void Exit()
    {
        base.Exit();
        _playerhealth.isStuned = false;
    }
}
