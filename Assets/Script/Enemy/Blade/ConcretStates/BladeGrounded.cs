using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeGrounded : BaseState
{
    public PlayerDetection _playerDetection;
    public Blade _sm;
    protected Rigidbody2D _rb;
    

    public BladeGrounded(string name, Blade stateMachine) : base(name, stateMachine)
    {
        _sm = (Blade)stateMachine;
    }
    // Start is called before the first frame update
    public override void Enter()
    {
        base.Enter();
        _playerDetection = _sm.GetComponentInChildren<PlayerDetection>();
        _rb = _sm.GetComponent<Rigidbody2D>();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Flip();
        if(_rb.velocity.y > 0.1f)
        {
        //StateMachine.ChangeState(_sm.bladeAirState);
        }

    }
    protected void Flip()
    {
        if (_sm.isFacingRight && _rb.velocity.x < -0.1f || !_sm.isFacingRight && _rb.velocity.x > 0.1f)
        {
            _sm.isFacingRight = !_sm.isFacingRight;
            //transform.Rotate(0f, 180f, 0f);
            Vector3 localscale = _sm.transform.localScale;
            localscale.x *= -1f;
            _sm.transform.localScale = localscale;
        }
    }

}
