using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirCombo : PlayerAttackState
{
    private PlayerMovementSM _sm;
    private float _horizontalInput;
    private float _attackPressedTimer;
    private float _attackPressedTimerMax = 0.5f;
    private float _comboTimeMax = 10f;
    private float _comboTimeMin = 0f;
    private float _comboTimer = 0f;
    private int _comboCount;
    protected Detector _detector;
    public PlayerAirCombo(PlayerMovementSM stateMachine) : base("PlayerAirCombo", stateMachine)
    {
        _sm = (PlayerMovementSM)stateMachine;
    }

    enum Attacks { attack0, attack1, attack2, attackUp0, attackUp1 }
    Attacks currentAttack;
    enum Weapon { knife, spear }
    Weapon currentWeapon;
    public override void Enter()
    {

        base.Enter();
        _detector = _sm.GetComponent<Detector>();
        _comboCount = 1;
        SetCurrentAttack(_comboCount);
        Attack();

        _attackPressedTimer = 0f;
        _comboTimer = 0f;


        _horizontalInput = 0;
        Vector2 vel = _sm.rb.velocity;
        vel.x = _horizontalInput * _sm.speed;
        _sm.rb.velocity = vel;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        _comboTimer += Time.deltaTime;
        if (_attackPressedTimer > 0f) _attackPressedTimer -= Time.deltaTime;
        if (Input.GetButtonDown("Attack")) _attackPressedTimer = _attackPressedTimerMax;

        if (_comboTimer > _comboTimeMax)
        {
            _comboTimer = 0f;
            stateMachine.ChangeState(_sm.idleState);
        }
        if (_detector.IsGrounded()) stateMachine.ChangeState(_sm.idleState);

    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (_attackPressedTimer > 0f && _comboTimer > _comboTimeMin)
        {
            _attackPressedTimer = 0f;
            _comboTimer = 0f;
            SetCurrentAttack(_comboCount);
            Attack();


        }


    }

    private void Attack()
    {

        switch (currentAttack)
        {
            case Attacks.attack0:
                _sm.animator.Play("Player_attack_airCombo_1");

                _comboTimeMax = 0.583f;
                _comboTimeMin = 0.42f;


                break;
            case Attacks.attack1:
                _sm.animator.Play("Player_attack_airCombo_2");
                _comboTimeMax = 0.583f;
                _comboTimeMin = 0.42f;


                break;
            case Attacks.attack2:
                _sm.animator.Play("Player_attack_airCombo_3");
                _comboTimeMax = 0.75f;
                _comboTimeMin = 0.5f;


                break;
            
        }
    }
    private void SetCurrentAttack(int comboCount)
    {

        switch (comboCount)
        {
            case 1:
                Debug.Log(_comboCount);
                currentAttack = Attacks.attack0;


                break;
            case 2:
                Debug.Log(_comboCount);
                currentAttack = Attacks.attack1;

                break;
            case 3:
                Debug.Log(_comboCount);
                currentAttack = Attacks.attack2;
                _comboCount = 0;

                break;

        }

        _comboCount++;


    }
}
