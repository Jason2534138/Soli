using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Foothit : BaseState
{
    private float stunTimeMax = 1f;
    private float stunTimer;
    private Health _health;
    private Animator _animator;
    public Foothit(FootSM stateMachine) : base("Foothit", stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        stunTimer = stunTimeMax;
        _health = ((FootSM)stateMachine)._health;
        _animator = ((FootSM)stateMachine)._animator;
        _animator.Play("Foot_Hit");

    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        stunTimer -= Time.deltaTime;
        if(stunTimer < 0f)
        {
            if (!_health.isStuned) stateMachine.ChangeState(((FootSM)stateMachine).footAggro);
            else stateMachine.ChangeState(((FootSM)stateMachine).footStun);
        }
            
    }

    public override void Exit()
    {
        base.Exit();
    }

}
