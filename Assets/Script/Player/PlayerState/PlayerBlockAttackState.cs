using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockAttackState : BaseState
{
    private PlayerMovementSM _sm;
    private float _timer;
    public PlayerBlockAttackState(PlayerMovementSM stateMachine) : base("PlayerBlockAttackState", stateMachine)
    {
        _sm = (PlayerMovementSM)stateMachine;
    }
    public override void Enter()
    {
        base.Enter();
        _timer = 0f;
        _sm.animator.Play("Player_block_attack");
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        _timer += Time.deltaTime;
        if (_timer > 0.5f) _sm.ChangeState(_sm.idleState);
    }
}
