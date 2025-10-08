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
        vel.x += _sm.transform.lossyScale.x > 0 ? 5f : -5f;
        _sm.rb.velocity = vel;
        _timer = 0f;
        _sm.animator.Play("Player_dash_attack");
        
        
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        _timer += Time.deltaTime;
        Vector2 x;
        x = _sm.rb.velocity;
        x.x -= Time.deltaTime;
        _sm.rb.velocity = x;
        if (_timer > 0.7f) _sm.ChangeState(_sm.idleState);
    }


}
