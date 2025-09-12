using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class BladeBaseState : BaseState
{
    #region 基本元件
    public Blade _sm;
    public Rigidbody2D _rb;
    public Health _health;
    public bool _isFacingRight;
    public Animator _animator;
    public bool isFacingRight = false;
    public float _speed = 10f;
    public int dir = -1;
    public bool _isWalking = true;
    public float _idleTime = 3f;
    public float _idleTimer;
    #endregion

    public BladeBaseState(string name ,Blade stateMachine) : base(name, stateMachine) 
    {
        _sm = (Blade)stateMachine; 
    }
    public override void Enter()
    {
        base.Enter();
        SetUp();
    }

    protected void Hit(){}
    protected void Die(){}
    protected void Flip()
    {
        if (isFacingRight || !isFacingRight)
        {
            isFacingRight = !isFacingRight;
            //transform.Rotate(0f, 180f, 0f);
            Vector3 localscale = _sm.transform.localScale;
            localscale.x *= -1f;
            _sm.transform.localScale = localscale;
        }
    }
    private void SetUp()
    {
        if (_health == null && _sm.GetComponent<Health>() != null)
        {
            _health = _sm.GetComponent<Health>();

        }
        _rb = _sm.GetComponent<Rigidbody2D>();
        _animator = _sm.GetComponent<Animator>();
        _health.SetUp();

        _health.healthSystem.Hit += Hit;
        _health.healthSystem.Die += Die;
    }
}
