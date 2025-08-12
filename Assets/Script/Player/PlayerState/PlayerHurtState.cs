using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHurtkState : BaseState
{
    private float stunTime;
    public PlayerHurtkState(PlayerMovementSM stateMachine) : base("PlayerHurtkState", stateMachine) { }
    public override void Enter()
    {
        base.Enter();
        ((PlayerMovementSM)stateMachine).rb.velocity = new Vector2(((PlayerMovementSM)stateMachine).isFacingRight? -10 : 10, 10);
        stunTime = 0f;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        ((PlayerMovementSM)stateMachine).animator.Play("Player_hurt");
        stunTime += Time.deltaTime;
        if (stunTime > 0.25f) ((PlayerMovementSM)stateMachine).ChangeState(((PlayerMovementSM)stateMachine).idleState);
    }
}
