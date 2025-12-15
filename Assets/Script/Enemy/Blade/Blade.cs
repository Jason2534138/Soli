using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Burst.CompilerServices;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;

public class Blade : StateMachine, IDamageable
{
    //此為敵人的狀態腳本，此腳本只負責聲明與切換各種狀態。
    //實際功能會在各狀態中。
    //由於此腳本繼承StateMachine，要是出現錯誤在這裡找不到試試看去StateMachine中找。

    
    public Rigidbody2D _rb;
    public Health _health;
    public Animator _animator;
    public PlayerDetection _playerDetection;

    #region 狀態(States)
    [HideInInspector]
    public BladeIdle bladeIdleState;
    [HideInInspector]
    public BladeAggroState bladeAggroState;
    [HideInInspector]
    public BladeAttackState bladeAttackState;
    [HideInInspector]
    public BladeStunState bladeStunState;
    [HideInInspector]
    public BladeAirState bladeAirState;
    #endregion
    private void Awake()
    {
        SetUp();
    }
    
    protected override BaseState GetInitialState()
    {
        return bladeIdleState;
    }
    private void SetUp()
    {
        _health = GetComponent<Health>();
        _rb = GetComponent<Rigidbody2D>();
        _health.SetUp(100);
        _animator = GetComponent<Animator>();
        _playerDetection = GetComponentInChildren<PlayerDetection>();
        //狀態在這裡設置，沒特別原因，主要想統一，敵人新狀態完成後記得在這裡加上去
        bladeIdleState = new BladeIdle(this);
        bladeAggroState = new BladeAggroState(this);
        bladeAttackState = new BladeAttackState(this);
        bladeStunState = new BladeStunState(this);
        bladeAirState = new BladeAirState(this);
        //基本物件
        _health.healthSystem.Die += Die;


    }

    public void OnHit(int damage)
    {
        _health.healthSystem.Damage(damage);
        Debug.Log(_health.healthSystem.GetHealth());
    }
    private void Die()
    {
        Destroy(this.gameObject);
    }
}

