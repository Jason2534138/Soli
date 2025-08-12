using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.VFX;
//using static UnityEditor.Searcher.SearcherWindow.Alignment;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerSpearBlock : BaseState
{

    private PlayerMovementSM _sm;
    public bool isBlocking;
    private float blockingTime;
    public PlayerSpearBlock(PlayerMovementSM stateMachine) : base("PlayerSpearBlock", stateMachine) 
    {
        _sm = (PlayerMovementSM)stateMachine;
    }
    public override void Enter()
    {
        base.Enter();
        isBlocking = true;
        blockingTime = 0;
        _sm.rb.velocity = new Vector2(0, 0);
       
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        blockingTime += Time.deltaTime;
        if (blockingTime > 0.5f) stateMachine.ChangeState(_sm.idleState);
       
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        _sm.animator.Play("Player_spear_block");
        
        

    }
    public override void Exit()
    {
        base.Exit();
        isBlocking = false;
    }

}
