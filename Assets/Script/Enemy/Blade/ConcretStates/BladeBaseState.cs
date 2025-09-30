using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class BladeBaseState : BaseState
{
    #region 基本元件
    protected Blade _sm;
    protected Rigidbody2D _rb;
    protected Health _health;
    protected Animator _animator;
    protected PlayerDetection _playerDetection;

    //此變數dir為角色的方向的定義，大部分轉向功能只要改變此數就可以達成
    
    private int _groundLayer = 1 << 6;

    protected float _speed = 10f;

    protected bool _isFacingRight = false;
    protected bool _isWalking = true;
    protected bool _grounded;
    #endregion

    public BladeBaseState(string name ,Blade stateMachine) : base(name, stateMachine) 
    {
        _sm = (Blade)stateMachine;
        _rb = _sm._rb;
        _health = _sm._health;
        _animator = _sm._animator;
        _playerDetection = _sm._playerDetection;
    }
    
    public override void Enter()
    {
        base.Enter();
        
        SetUp();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        //Debug.Log(_isFacingRight);
        GroundCheck();
    }
    protected void Hit(){}
    protected void Die(){}
    protected void Flip()
    {
            _isFacingRight = !_isFacingRight;
            Vector3 localscale = _sm.transform.localScale;
            if(_isFacingRight) localscale.x = -1;
            else localscale.x = 1;
        _sm.transform.localScale = localscale;
    }
    private void GroundCheck()
    {
        float timer = 0f;
        if (Physics2D.Raycast(_sm.transform.position, Vector2.down, 3.5f, _groundLayer)) timer += Time.deltaTime;
        else timer = 0f;
        if (timer > 0.5f) _grounded = false;
        else _grounded = true;
    }
    private void SetUp()
    {
        
        _health.healthSystem.Hit += Hit;
        _health.healthSystem.Die += Die;
    }
}
