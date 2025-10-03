using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBaseState : BaseState
{
    protected FlySM _sm;
    protected PlayerDetection _playerDetection;
    protected Rigidbody2D _rb;
    protected Animator _animator;
    protected Health _health;
    protected Transform[] _patrolPoints;
    protected int _currentPatrolPoint;
    protected Vector2 _offset;
    
    public FlyBaseState(string name, FlySM stateMachine) : base(name, stateMachine)
    {
        _sm = (FlySM)stateMachine;
        _health = _sm._health;
        _rb = _sm._rb;
        _playerDetection = _sm._playerDetection;
        _animator = _sm._animator;
        
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
        
    }

    protected void Flip()
    {
        _sm.isFacingRight = !_sm.isFacingRight;
        
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
