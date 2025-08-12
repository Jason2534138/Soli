using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBaseState : BaseState
{
    protected FlySM _sm;
    public PlayerDetection _playerDetection;
    public Rigidbody2D _rb;
    public Animator _animator;
    public bool isFacingRight = false;
    public Health _health;
    public FlyBaseState(string name, FlySM stateMachine) : base(name, stateMachine)
    {
        _sm = (FlySM)stateMachine;

    }
    public override void Enter()
    {
        base.Enter();
        _playerDetection = _sm.GetComponentInChildren<PlayerDetection>();
        _health = _sm.GetComponent<Health>();
        _rb = _sm.GetComponent<Rigidbody2D>();
        _playerDetection = _sm.GetComponentInChildren<PlayerDetection>();
        _animator = _sm.GetComponent<Animator>();
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
