using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FootBlocked : BaseState
{
    private Attack _attack;
    public FootBlocked(FootSM stateMachine) : base("FootBlocked", stateMachine) { }
    private float stunTime;
    public override void Enter()
    {
        base.Enter();
        _attack = ((FootSM)stateMachine).GetComponentInChildren<Attack>();
        _attack.isBlocked = false;
        stunTime = 0;
        ((FootSM)stateMachine).rb.velocity = new Vector2(((FootSM)stateMachine).isFacingRight?  -10 : 10, 10);
        
        
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        stunTime += Time.deltaTime;
        if (stunTime > 1f) ((FootSM)stateMachine).ChangeState(((FootSM)stateMachine).footAggro);
    }
    public override void Exit()
    {
        base.Exit();
        
    }
}
