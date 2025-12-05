using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrocSM : StateMachine
{
    public bool actionOver = false;

    public CrocSM _sm;
    public Animator _animator;
    public Rigidbody2D _rb;

    public Health _health;
    public PlayerDetection _playerDetection;

    public bool _isFacingRight;

    public AttackDetection _attackDetection_bite;
    public AttackDetection _attackDetection_spike;
    public AttackDetection _attackDetection_charge;

    [HideInInspector] 
    public CrocIdleState crocIdleState;
    [HideInInspector]
    public CrocAttackState crocAttackState;
    [HideInInspector]
    public CrocAggroState crocAggroState;
    [HideInInspector]
    public CrocMovingState crocMovingState;

    private void Awake()
    {
        SetUp();
    }
    private void SetUp()
    {
        _playerDetection = GetComponentInChildren<PlayerDetection>();
        _health = GetComponent<Health>();
        _health.SetUp(100);
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _isFacingRight = false;

        _health.healthSystem.Die += Die;

        crocIdleState = new CrocIdleState(this);
        crocAggroState = new CrocAggroState(this);
        crocAttackState = new CrocAttackState(this);
        crocMovingState = new CrocMovingState(this);
    }
    protected override BaseState GetInitialState()
    {
        return crocIdleState;
    }
    protected void Die()
    {

    }
    public virtual void ActionOver()
    {
        actionOver = true;
    }
}
