using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashAttack : BaseState
{

    private PlayerMovementSM _sm;
    private float _timer;
    
    public PlayerDashAttack(PlayerMovementSM stateMachine) : base("PlayerDashAttack", stateMachine)
    {
        _sm = (PlayerMovementSM)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        Vector2 vel = _sm.rb.velocity;
        vel.x += _sm.isFacingRight ? 50f : -50f;
        _sm.rb.velocity = vel;
        _timer = 0f;
        _sm.animator.Play("Player_dash_attack");
        
        
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        _timer += Time.deltaTime;
        if (_sm._actionOver)
        {
            _sm._actionOver = false;
            _sm.rb.velocity = Vector2.zero;
        }
        if (_timer > 0.7f) _sm.ChangeState(_sm.idleState);
    }


}
