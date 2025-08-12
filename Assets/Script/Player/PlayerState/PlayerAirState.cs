using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerAirState : BaseState
{
    private float _horizontalInput;
    private Detector _detector;

    private PlayerMovementSM _sm;
    public PlayerAirState(PlayerMovementSM stateMachine) : base("PlayerAirState", stateMachine)
    {
        _sm = (PlayerMovementSM)stateMachine;
    }
    public override void Enter()
    {
        base.Enter();
        _detector = ((PlayerMovementSM)stateMachine).GetComponent<Detector>();

    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        _horizontalInput = Input.GetAxis("Horizontal");
        if (_detector.IsGrounded())
        {
            if (_horizontalInput > Mathf.Epsilon) stateMachine.ChangeState(_sm.movingState);
            else stateMachine.ChangeState(_sm.idleState);
        }
        if (Input.GetButtonDown("Attack")) stateMachine.ChangeState(((PlayerMovementSM)stateMachine).airComboState);
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        Vector2 vel = _sm.rb.velocity;
        vel.x = _horizontalInput * _sm.speed;
        _sm.rb.velocity = vel;
        if (_sm.rb.velocity.y > 0.01f) _sm.animator.Play("Player_jump_up 0");
        else _sm.animator.Play("Player_fall 0");
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
