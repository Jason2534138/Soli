using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerAirState : BaseState
{
    private float _horizontalInput;
    private Detector _detector;
    
    private PlayerMovementSM _sm;
    private WallDetector _wallDetector;
    public PlayerAirState(PlayerMovementSM stateMachine) : base("PlayerAirState", stateMachine)
    {
        _sm = (PlayerMovementSM)stateMachine;
        
    }
    public override void Enter()
    {
        base.Enter();
        _wallDetector = _sm.GetComponentInChildren<WallDetector>();
        _detector = ((PlayerMovementSM)stateMachine).GetComponent<Detector>();

    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        _horizontalInput = Input.GetAxis("Horizontal");
        
        if (Input.GetButtonDown("Attack")) stateMachine.ChangeState(((PlayerMovementSM)stateMachine).airComboState);
        if (Input.GetButtonDown("Jump") && _sm._jumpLeft > 0)
        {
            
            _sm._jumpLeft -= 1;
            
            Vector2 vel2;
            vel2 = ((PlayerMovementSM)stateMachine).rb.velocity;
            vel2.y = _sm._jumpForce;
            ((PlayerMovementSM)stateMachine).rb.velocity = vel2;
        }
        Vector2 vel = _sm.rb.velocity;
        vel.x = _horizontalInput * _sm.speed;
        _sm.rb.velocity = vel;
        if (_sm.rb.velocity.y > 0.01f) _sm.animator.Play("Player_jump_up 0");
        else _sm.animator.Play("Player_fall 0");
        Flip();
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (_sm._ledgeDetection.CanLedgeClimb() && Input.GetButton("Up"))
        {
            stateMachine.ChangeState(_sm.playerLedgeClimb);
            return;
        }
        if (_detector.IsWalled() && _sm.rb.velocity.y < 1f && Input.GetAxisRaw("Horizontal") != 0 && _wallDetector.AttachObj != null)
        {
            stateMachine.ChangeState(_sm.wallJumpState);
            return;
        }
        if (_detector.IsGrounded())
        {
            if (_horizontalInput > Mathf.Epsilon)
            {
                stateMachine.ChangeState(_sm.movingState);
                return;
            }
            else stateMachine.ChangeState(_sm.idleState);
        }

       

    }
    public override void Exit()
    {
        base.Exit();
        
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
