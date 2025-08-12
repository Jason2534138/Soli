using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.VFX;
//using static UnityEditor.Searcher.SearcherWindow.Alignment;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerMoving : PlayerGrounded
{
    public PlayerMoving(PlayerMovementSM stateMachine) : base("PlayerMoving", stateMachine) 
    {
        _sm = (PlayerMovementSM)stateMachine;
    }
    public override void Enter()
    {
        base.Enter();
       
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        _horizontalInput = Input.GetAxis("Horizontal");
        if (Mathf.Abs(_horizontalInput) < Mathf.Epsilon)
        {
            stateMachine.ChangeState(_sm.idleState);
            
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        Vector2 vel = _sm.rb.velocity;
        vel.x = _horizontalInput * _sm.speed;
        _sm.rb.velocity = vel;
        
        Flip();

    }
    private void Flip()
    {
        if (_sm.isFacingRight && _horizontalInput < 0f || !_sm.isFacingRight && _horizontalInput > 0f)
        {
            _sm.isFacingRight = !_sm.isFacingRight;
            //transform.Rotate(0f, 180f, 0f);
            Vector3 localscale = _sm.transform.localScale;
            localscale.x *= -1f;
            _sm.transform.localScale = localscale;
        }
    }
}
