using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FlyAttackState : FlyBaseState
{
    public FlyAttackState(FlySM stateMachine) : base("FlyAttackState", stateMachine){}

    private GameObject _target;
    private int _dir;
    private Vector2 _attackDir;
    private Vector2 _startAngle;
    private Vector2 _endAngle;
    
    private float _speed;
    private float _t;
    private float _attackTIme;
    private float _startUp;
    private GameObject _hitBox;

    public override void Enter()
    {
        base.Enter();
        Debug.Log("attack");
        _hitBox = _sm.transform.Find("HitBox").gameObject;
        _hitBox.SetActive(true);
        _t = 0f;
        _attackTIme = 1.5f;
        _startUp = 0.5f;
        _target = GameObject.FindGameObjectWithTag("Player");
        _dir = _target.transform.position.x - _sm.transform.position.x > 0f ? 1 : -1;
        _startAngle = new Vector2(_target.transform.position.x - _sm.transform.position.x, 2 * (_target.transform.position.y - _sm.transform.position.y));
        _endAngle = new Vector2(_startAngle.x, -(_startAngle.y));
        
        _speed = Mathf.Abs((_sm.transform.position.x) - (_target.transform.position.x)) * 6.283f / 4 * 2;

    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Flip(); 
        
        _startUp -= Time.deltaTime;
        _attackTIme -= Time.deltaTime;
        if ((Physics2D.Raycast(_sm.transform.position, Vector2.down, 10f, 1 << 6) == false && _sm._rb.velocity.y > -0.1f) || Physics2D.Raycast(_sm.transform.position, Vector2.up, 3f, 1 << 6) == true  || _attackTIme < 0f && _startUp < 0f) stateMachine.ChangeState(_sm.flyAggroState);
        if(_t < 1f) _t += Time.deltaTime / (-(_startAngle.y) / 30);
        
        _attackDir = new Vector2(Mathf.Lerp(_startAngle.x, _endAngle.x, _t), Mathf.Lerp(_startAngle.y, _endAngle.y, _t));
        
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        _rb.velocity = _attackDir.normalized * _speed;
    }
    public override void Exit()
    {
        base.Exit();
        _hitBox.SetActive(false);
        _rb.velocity = Vector2.zero;
    }
}
