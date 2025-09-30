using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBaseState : BaseState
{
    protected FlySM _sm;
    protected PlayerDetection _playerDetection;
    protected Rigidbody2D _rb;
    protected Animator _animator;
    protected bool _isFacingRight = false;
    protected Health _health;
    protected Transform[] _patrolPoints;
    protected int _currentPatrolPoint;
    protected Vector2 _offset;
    protected Vector2 _attackDir;
    public FlyBaseState(string name, FlySM stateMachine) : base(name, stateMachine)
    {
        _sm = (FlySM)stateMachine;
        _health = _sm._health;
        _rb = _sm._rb;
        _playerDetection = _sm._playerDetection;
        _animator = _sm._animator;
        _isFacingRight = _sm.isFacingRight;
        _patrolPoints = _sm.patrolPoints;
        _currentPatrolPoint = _sm.currentPatrolPoint;
        _offset = _sm._offset;

    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if ((_rb.velocity.x > 0.1f && !_isFacingRight) || (_rb.velocity.x < 0.1f && _isFacingRight))
        {
            Flip();
        }
    }

    protected void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 localscale = _sm.transform.localScale;
        localscale.x *= -1;
        
        _sm.transform.localScale = localscale;
    }
    protected void FlyUp()
    {
        if (Physics2D.Raycast(_sm.transform.position, Vector2.down, 15f, 1 << 6) == true)
        {
            Vector2 vel = _rb.velocity;
            vel.y = 2f;
            _rb.velocity = vel;
        }
        else
        {
            Vector2 vel = _rb.velocity;
            vel.y = 0f;
            _rb.velocity = vel;
        }
    }

}
