using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashAttack : PlayerGrounded
{
    
    private float _timer;
    public PlayerDashAttack(PlayerMovementSM stateMachine) : base("PlayerDashAttack", stateMachine)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        _sm.animator.Play("Player_dash_attack");
        _timer = 0f;
        
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        _timer += Time.deltaTime;
        Vector2 x;
        x = _sm.rb.velocity;
        x.x -= Time.deltaTime;
        _sm.rb.velocity = x;
        if (_timer > 0.5f) _sm.ChangeState(_sm.idleState);
    }
}
