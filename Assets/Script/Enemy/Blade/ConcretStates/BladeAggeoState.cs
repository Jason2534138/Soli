using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BladeAggroState : BladeBaseState
{
    private float aggroTimeMax = 5f;
    private float aggroTimer;
    private GameObject target;
    private Attack _attack;
    private AttackDetection attackDetection;
    public BladeAggroState(Blade stateMachine) : base("BladeAggroState", stateMachine) { }
    // Start is called before the first frame update
    public override void Enter()
    {
        base.Enter();
        SetUp();
        _animator.Play("Blade_run");
    }
    
    public override void LogicUpdate()
    {
        
        base.LogicUpdate();
        
        HandleAttack();
        HandleDeaggro();
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        HandlePhysics();   
    }
    public override void Exit()
    {
        base.Exit();
    }
    private void SetUp()
    {
        aggroTimer = aggroTimeMax;
        if (!_attack) _attack = _sm.GetComponentInParent<Attack>();
        if (!attackDetection) attackDetection = _sm.GetComponentInChildren<AttackDetection>();
        if (!target) target = GameObject.FindGameObjectWithTag("Player");
    }
    private void HandlePhysics()
    {
        if((target.transform.position.x > _sm.gameObject.transform.position.x && !_isFacingRight) || (target.transform.position.x < _sm.gameObject.transform.position.x && _isFacingRight)) Flip();
        int dir;
        if (_isFacingRight) dir = 1;
        else dir = -1;
        _rb.velocity = new Vector2(_speed * dir, _rb.velocity.y);
        
    }
    private void HandleDeaggro()
    {
        if (_playerDetection.isSeeingPlayer)
        {
            aggroTimer = aggroTimeMax;
        }
        else
        {
            aggroTimer -= Time.deltaTime;
            if (aggroTimer < 0f) stateMachine.ChangeState(_sm.bladeIdleState);
        }
    }
    private void HandleAttack()
    {
        if (attackDetection.hasAttackTarget == true)
        {
            stateMachine.ChangeState(_sm.bladeAttackState);
        }
    }
}
