using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BladeAggroState : BladeGrounded
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
        _attack = _sm.GetComponentInParent<Attack>();
        attackDetection = _sm.GetComponentInChildren<AttackDetection>();
        aggroTimer = aggroTimeMax;
        target = GameObject.FindGameObjectWithTag("Player");
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        _sm._animator.Play("Blade_run");

        
        if(attackDetection.hasAttackTarget == true)
        {
            
            stateMachine.ChangeState(_sm.bladeAttackState);

        }
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
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        int dir = target.transform.position.x > _sm.gameObject.transform.position.x ? 1 : -1;

        _rb.velocity = new Vector2(_sm._speed * dir, _rb.velocity.y);

    }
    public override void Exit()
    {
        base.Exit();
    }
}
