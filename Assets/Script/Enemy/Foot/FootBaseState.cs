using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FootBasseState : BaseState
{
    protected FootSM _sm;
    protected Rigidbody2D _rb;
    protected float _speed = 5f;
    protected Health _health;
    protected PlayerDetection _playerDetection;
    protected Animator _animator;
    protected bool _isFacingRight;
    protected bool _grounded;

    private int _groundLayer = 1 << 6;

    public FootBasseState(string name, FootSM stateMachine) : base(name, stateMachine)
    {
        _sm = (FootSM)stateMachine;
        _rb = _sm.rb;
        _playerDetection = _sm._playerDetection;
        _health = _sm._health;
        _animator = _sm._animator;
        

    }
    public override void Enter()
    {
        base.Enter();
        SetUp();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        GroundCheck();
        if(Input.GetKeyDown(KeyCode.T)) _sm.ChangeState(_sm.footHit);
    }
    private void GroundCheck()
    {
        float timer = 0f;
        if (Physics2D.Raycast(_sm.transform.position, Vector2.down, 3.5f, _groundLayer)) timer += Time.deltaTime;
        else timer = 0f;
        if (timer > 0.5f) _grounded = false;
        else _grounded = true;
    }
    protected void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 localscale = _sm.transform.localScale;
        if (_isFacingRight) localscale.x = -1;
        else localscale.x = 1;
        _sm.transform.localScale = localscale;
    }
    private void SetUp()
    {

        
        _health.healthSystem.Die += Die;
    }
    private void Stun(object sender, EventArgs e)
    {

        stateMachine.ChangeState(_sm.footStun);
    }
    
    protected void Die()
    {

    }

}
