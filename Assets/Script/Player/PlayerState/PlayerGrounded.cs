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

    public PlayerGrounded(string name, PlayerMovementSM stateMachine) : base(name, stateMachine){ }
    public override void Enter()
    {
        base.Enter();
        _detector  = ((PlayerMovementSM)stateMachine).GetComponent<Detector>();
    }
    public override void LogicUpdate()
    {
        
        base.LogicUpdate();
        _horizontalInput = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Attack"))
        {
            if (_detector.IsGrounded())
            {
                stateMachine.ChangeState(((PlayerMovementSM)stateMachine).groundComboState);
            }
            
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (_detector.IsGrounded())
            {
                stateMachine.ChangeState(((PlayerMovementSM)stateMachine).spearBlockState);
            }

        }

        if (!_detector.IsGrounded()) stateMachine.ChangeState(((PlayerMovementSM)stateMachine).airState);

    }
    public override void PhysicsUpdate()
    {
        if (Input.GetButton("Jump") && _detector.IsGrounded())
        {
            base.PhysicsUpdate();
            Vector2 vel;
            vel = ((PlayerMovementSM)stateMachine).rb.velocity;
            vel.y = 40f;
            ((PlayerMovementSM)stateMachine).rb.velocity = vel;
            stateMachine.ChangeState(((PlayerMovementSM)stateMachine).airState);
        }
        
    }
}
