using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FootBasseState : BaseState
{
    protected FootSM _sm;
    protected Rigidbody2D _rb;
    protected float speed = 5f;
    
    protected Health _health;



    public FootBasseState(string name, FootSM stateMachine) : base(name, stateMachine)
    {
        _sm = (FootSM)stateMachine;
    }
    public override void Enter()
    {
        base.Enter();
        _health = _sm.gameObject.GetComponent<Health>();
        _rb = _sm.gameObject.GetComponent<Rigidbody2D>();
        _health.ZeroStun += Stun;
        
        _health.HitStun += Hit;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Flip();


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
    /*protected void CliffWallDetect()
    {
        if (Physics2D.Raycast(_sm.transform.position, , 1f, 1 << 6) == false)
        {
            
        }
    }*/
    private void Stun(object sender, EventArgs e)
    {

        stateMachine.ChangeState(_sm.footStun);
    }
    private void Hit(object sender, EventArgs e)
    {
        if(!_health.isStuned) stateMachine.ChangeState(_sm.footHit);
    }
    

}
