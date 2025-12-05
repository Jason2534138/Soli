using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CrocAttackState : CrocBaseState
{
    private int _attackIndex;
    private GameObject target;
    public CrocAttackState(CrocSM stateMachine) : base("CrocAttackState", stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        if (!target) target = GameObject.FindGameObjectWithTag("Player");
        if (_sm._attackDetection_bite.hasAttackTarget)
        {
            _attackIndex = 0;
        }else if(_sm._attackDetection_spike.hasAttackTarget){
            _attackIndex = 1;
        }else
        {
            _attackIndex = 2;
        }
        Debug.Log("attack" + _attackIndex);
        
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(_sm.actionOver)ActionOver();
        
        switch (_attackIndex)
        {
            case 0:
                Bite();
                break;
            case 1:
                Spike();
                break;
            case 2:
                Charge();

                break;
        }

    }
    private void Bite()
    {
        _rb.velocity = Vector2.zero;
        _animator.Play("Car_Attack");
    }
    private void Spike()
    {
        _rb.velocity = Vector2.zero;
        _animator.Play("Car_SpikeAttack");
    }
    private void Charge()
    {
        _animator.Play("Car_DashAttack");
        _rb.velocity = new Vector2(_sm._isFacingRight ? 24f : -24f, _rb.velocity.y);
        if ((_sm.transform.position.x - target.transform.position.x > 5f && _sm._isFacingRight) || (_sm.transform.position.x - target.transform.position.x  < -5f && !_sm._isFacingRight)) Flip();
        if (_sm._attackDetection_bite.hasAttackTarget)
        {
            _attackIndex = 0;
        }
    }
    public override void ActionOver()
    {
        base.ActionOver();
        stateMachine.ChangeState(_sm.crocAggroState);
        Debug.Log("attackOver");
    }
}
