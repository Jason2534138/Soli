using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BladeAttackState : BladeGrounded
{
    private float attackDuration;
    public BladeAttackState(Blade stateMachine) : base("BladeAttackState", stateMachine) { }
    // Start is called before the first frame update
    public override void Enter()
    {
        base.Enter();
        attackDuration = 2f;
        _sm._animator.Play("Blade_attack1");
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        attackDuration -= Time.deltaTime;
        if (attackDuration < 0)
        {
            stateMachine.ChangeState(_sm.bladeAggroState);
        }
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
