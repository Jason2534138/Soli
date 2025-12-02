using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class PlayerWallJumpState : BaseState
{
    private PlayerMovementSM _sm;
    private Detector _detector;
    private float _horizontalInput;
    private float _gravity;
    private float exitTimer;
    private WallDetector _wallDetector;
    private bool _ledgeClimb = false;
    private GameObject collisionObj;
    public PlayerWallJumpState(PlayerMovementSM stateMachine) : base("PlayerWallJumpState", stateMachine)
    {
        _sm = (PlayerMovementSM)stateMachine;
        _gravity = _sm.rb.gravityScale;
    }
    public override void Enter()
    {
        base.Enter();
        _wallDetector = _sm.GetComponentInChildren<WallDetector>();
        _sm.rb.velocity = Vector2.zero;
        if(_wallDetector.AttachObj != null)_sm.transform.parent = _wallDetector.AttachObj.transform;
        _sm.rb.gravityScale = 0f;
        _detector = _sm.GetComponent<Detector>();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        _sm.animator.Play("Player_wall");
        _horizontalInput = Input.GetAxis("Horizontal");
        
        if (_detector.IsGrounded())
        {
            stateMachine.ChangeState(_sm.idleState);
        }
        
        if (Input.GetButtonDown("Jump"))
        {
            
            if (_sm._wallJumpLeft > 0)
            {
                _sm._wallJumpLeft -= 1;
                Vector2 vel;
                vel = ((PlayerMovementSM)stateMachine).rb.velocity;
                vel.y = _sm._jumpForce;
                //vel.x = _sm.transform.localScale.x * -10f;
                ((PlayerMovementSM)stateMachine).rb.velocity = vel;
                stateMachine.ChangeState(_sm.airState);
                Flip();
            }
            else if(_sm._jumpLeft > 0)
            {
                _sm._jumpLeft -= 1;
                _sm._wallJumpLeft -= 1;
                Vector2 vel;
                vel = ((PlayerMovementSM)stateMachine).rb.velocity;
                vel.y = _sm._jumpForce;
                //vel.x = _sm.transform.localScale.x * -10f;
                ((PlayerMovementSM)stateMachine).rb.velocity = vel;
                stateMachine.ChangeState(_sm.airState);
                Flip();
            }
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (!_detector.IsWalled())
        {
            exitTimer -= Time.deltaTime;
            if (exitTimer < 0f) stateMachine.ChangeState(_sm.airState);
        }
        else exitTimer = 0.08f;
        if (Input.GetButtonDown("Down"))
        {
            stateMachine.ChangeState(_sm.airState);
        }
        if (_sm._ledgeDetection.CanLedgeClimb() && Input.GetButton("Up"))
        {
            _ledgeClimb = true;
            stateMachine.ChangeState(_sm.playerLedgeClimb);
            return;
        }
    }
    public override void Exit()
    {
        base.Exit();
        _sm.rb.gravityScale = _gravity;
        if(!_ledgeClimb)_sm.transform.parent = null;
        _ledgeClimb = false;
    }
    private void Flip()
    {
        if (_sm.isFacingRight && _sm.rb.velocity.x < 0f || !_sm.isFacingRight && _sm.rb.velocity.x > 0f)
        {
            _sm.isFacingRight = !_sm.isFacingRight;
            _sm.transform.Rotate(0f, 180f, 0f);
            
        }
    }
}
