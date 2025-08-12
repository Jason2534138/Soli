using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerIdle : PlayerGrounded
{
    

    public PlayerIdle (PlayerMovementSM stateMachine) : base("PlayerIdle", stateMachine){ }
    public override void Enter()
    {
        base.Enter();
        _horizontalInput = 0;
       
        Vector2 vel = ((PlayerMovementSM)stateMachine).rb.velocity;
        vel.x = 0;
        ((PlayerMovementSM)stateMachine).rb.velocity  = vel;
    }
    public override void LogicUpdate()
    {
        
        base.LogicUpdate();
        _horizontalInput = Input.GetAxis("Horizontal");


        
        if (Mathf.Abs(_horizontalInput) > Mathf.Epsilon)
        {
            stateMachine.ChangeState(((PlayerMovementSM)stateMachine).movingState);
        }
    }
    public override void PhysicsUpdate()
    {
        if (Input.GetButtonDown("Jump") && _detector.IsGrounded())
        {
            Vector2 vel;
            vel = ((PlayerMovementSM)stateMachine).rb.velocity;
            vel.y = 40f;
            ((PlayerMovementSM)stateMachine).rb.velocity = vel;

        }
        base.PhysicsUpdate();
        ((PlayerMovementSM)stateMachine).animator.Play("Player_idle");
    }
}
