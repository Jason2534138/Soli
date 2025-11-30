using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLedgeClimb : BaseState
{
    private PlayerMovementSM _sm;
    private float _horizontalInput;
    private Vector2 _offset1 = new Vector2(1.01f, 0.98f);
    private Vector2 _offset2 = new Vector2(5f, 3f);

    private Vector2 _climbBebunPosition;
    private Vector2 _climbOverPosition;
    private float _gravity;

    private WallDetector _wallDetector;
    //private bool _canGrabLedge = true;
    //private bool _canClimb; 
    public PlayerLedgeClimb(PlayerMovementSM stateMachine) : base("PlayerLedgeClimb", stateMachine)
    {
        _sm = (PlayerMovementSM)stateMachine;
    }
    public override void Enter()
    {
        base.Enter();
        _wallDetector = _sm.GetComponentInChildren<WallDetector>();
        _sm.rb.velocity = Vector2.zero;
        if (_wallDetector.AttachObj != null) _sm.transform.parent = _wallDetector.AttachObj.transform;
        _gravity = _sm.rb.gravityScale;
        _sm.rb.gravityScale = 0f;
        Vector2 ledgePosition = _sm.GetComponentInChildren<LedgeDetection>().transform.position;
        if (_sm.isFacingRight)
        {
            _climbBebunPosition = ledgePosition + _offset1;
            
        }
        else
        {
            _climbBebunPosition = new Vector2(ledgePosition.x - _offset1.x, ledgePosition.y + _offset1.y);
            
        }
        _sm.transform.position = _climbBebunPosition;
        _sm.animator.Play("Player_climb");
        //Debug.Log(_sm.transform.position);
        Debug.Log(_climbOverPosition);
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (_sm._actionOver)
        {
            _sm._actionOver = false;
            Vector2 ledgePosition = _sm.GetComponentInChildren<LedgeDetection>().transform.position;
            if (_sm.isFacingRight)
            {
                _climbOverPosition = ledgePosition + _offset2;
            }
            else
            {
                _climbOverPosition = new Vector2(ledgePosition.x - _offset2.x, ledgePosition.y + _offset2.y);
            }
            _sm.transform.position = _climbOverPosition;
            _sm.animator.Play("Player_idle 0");
            stateMachine.ChangeState(_sm.idleState);
        }
    }
    public override void Exit()
    {
        base.Exit();
        _sm.rb.gravityScale = _gravity;
        _sm.transform.parent = null;
    }

}
