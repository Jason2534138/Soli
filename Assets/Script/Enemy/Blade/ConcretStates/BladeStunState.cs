using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeStunState : BaseState
{
    private Blade _sm;
    private PlayerHealth _health;
    private float recoverTime;
    public BladeStunState(Blade stateMachine) : base("BladeStunState", stateMachine) { _sm = (Blade)stateMachine; }
    // Start is called before the first frame update
    public override void Enter()
    {
        base.Enter();
        _health = _sm.gameObject.GetComponent<PlayerHealth>();
        _health.isStuned = true;
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
        _health.isStuned = false;
        

    }
}
