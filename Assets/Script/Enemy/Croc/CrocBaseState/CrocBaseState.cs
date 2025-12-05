using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrocBaseState : BaseState
{
    protected CrocSM _sm;
    protected Animator _animator;
    protected Rigidbody2D _rb;

    protected Health _health;
    protected PlayerDetection _playerDetection;

    public CrocBaseState (string name, CrocSM stateMachine) : base(name, stateMachine)
    {
        _sm = (CrocSM)stateMachine;
        _rb = _sm._rb;
        _playerDetection = _sm._playerDetection;
        _health = _sm._health;
        _animator = _sm._animator;
    }

    public override void Enter()
    {
        base.Enter();
        
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
    
    protected void Flip()
    {
        _sm._isFacingRight = !_sm._isFacingRight;
        Vector3 localscale = _sm.transform.localScale;
        if (_sm._isFacingRight) localscale.x = -1;
        else localscale.x = 1;
        _sm.transform.localScale = localscale;
    }
    public virtual void ActionOver()
    {
        _sm.actionOver = false;
    }
}
