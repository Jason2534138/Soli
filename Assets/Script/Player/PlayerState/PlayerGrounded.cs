using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerGrounded : BaseState
{
    protected float _horizontalInput;
    protected Detector _detector;
    protected PlayerMovementSM _sm;
    private float _jumpForce;

    public PlayerGrounded(string name, PlayerMovementSM stateMachine) : base(name, stateMachine){ }
    public override void Enter()
    {
        base.Enter();
        
        _sm = ((PlayerMovementSM)stateMachine);
        _sm._jumpLeft = _sm._maxJump;
        _sm._wallJumpLeft = _sm._maxWallJump;
        _detector  = ((PlayerMovementSM)stateMachine).GetComponent<Detector>();
        _jumpForce = ((PlayerMovementSM)stateMachine)._jumpForce;
    }
    public override void LogicUpdate()
    {
        
        base.LogicUpdate();
        _horizontalInput = Input.GetAxis("Horizontal");

        
            if (Input.GetButtonDown("Attack"))
            {
                 stateMachine.ChangeState(((PlayerMovementSM)stateMachine).groundComboState);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {

                stateMachine.ChangeState(((PlayerMovementSM)stateMachine).spearBlockState);
            }
        
        
        if (Input.GetButtonDown("Dash"))
        {
               //只在此可衝刺
               stateMachine.ChangeState(((PlayerMovementSM)stateMachine).dashing);
        }
        if (!_detector.IsGrounded()) stateMachine.ChangeState(((PlayerMovementSM)stateMachine).airState);

    }
    public override void PhysicsUpdate()
    {
        if (Input.GetButton("Jump"))
        {
            base.PhysicsUpdate();
            Vector2 vel;
            vel = ((PlayerMovementSM)stateMachine).rb.velocity;
            vel.y = _jumpForce;
            ((PlayerMovementSM)stateMachine).rb.velocity = vel;
            stateMachine.ChangeState(((PlayerMovementSM)stateMachine).airState);
        }
        
    }
}
