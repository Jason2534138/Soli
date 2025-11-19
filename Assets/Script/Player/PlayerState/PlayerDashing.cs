using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashing : PlayerGrounded
{
    
    private float _timer;
    
    public PlayerDashing(PlayerMovementSM stateMachine) : base("PlayerDashing", stateMachine)
    {
        _sm = (PlayerMovementSM)stateMachine;
    }
    public override void Enter()
    {
        base.Enter();
        Vector2 vel = _sm.rb.velocity;
        vel.x += _sm.isFacingRight? 15f : -15f;
        _sm.rb.velocity = vel;
        _timer = 0f;
        _sm.animator.Play("Player_dash 0");
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        _timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_detector.IsGrounded())
            {
                stateMachine.ChangeState(((PlayerMovementSM)stateMachine).spearBlockState);
            }

        }
        if (Input.GetMouseButtonDown(0)) _sm.ChangeState(_sm.dashAttack);
        if (_timer > 0.5f) _sm.ChangeState(_sm.idleState);
    }
}
