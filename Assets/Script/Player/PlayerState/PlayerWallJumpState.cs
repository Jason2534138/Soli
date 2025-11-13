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
        //if (_wallDetector.AttachObj != null) _sm.gameObject.transform.parent = _wallDetector.AttachObj.transform;
        _sm.rb.gravityScale = 0f;
        _sm.rb.velocity = Vector2.zero;
        _detector = _sm.GetComponent<Detector>();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Flip();
        _sm.animator.Play("Player_wall");
        _horizontalInput = Input.GetAxis("Horizontal");
        if (_sm.isFacingRight && _horizontalInput < 0f || !_sm.isFacingRight && _horizontalInput > 0f)
        {
            exitTimer -= Time.deltaTime;
            if (exitTimer < 0f) stateMachine.ChangeState(_sm.airState);
        }
        else exitTimer = 0.08f;
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
                vel.x = _sm.transform.localScale.x * -10f;
                ((PlayerMovementSM)stateMachine).rb.velocity = vel;
                stateMachine.ChangeState(_sm.airState);
            }
            else if(_sm._jumpLeft > 0)
            {
                _sm._jumpLeft -= 1;
                _sm._wallJumpLeft -= 1;
                Vector2 vel;
                vel = ((PlayerMovementSM)stateMachine).rb.velocity;
                vel.y = _sm._jumpForce;
                vel.x = _sm.transform.localScale.x * -10f;
                ((PlayerMovementSM)stateMachine).rb.velocity = vel;
                stateMachine.ChangeState(_sm.airState);
            }
        }
    }
    public override void Exit()
    {
        base.Exit();
        _sm.rb.gravityScale = _gravity;
        _sm.transform.parent = null;    
    }
    private void Flip()
    {
        if (_sm.isFacingRight && _sm.rb.velocity.x < 0f || !_sm.isFacingRight && _sm.rb.velocity.x > 0f)
        {
            _sm.isFacingRight = !_sm.isFacingRight;
            //transform.Rotate(0f, 180f, 0f);
            Vector3 localscale = _sm.transform.localScale;
            localscale.x *= -1f;
            _sm.transform.localScale = localscale;
        }
    }
}
