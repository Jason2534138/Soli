using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Burst.CompilerServices;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;

public class Blade : StateMachine
{
    //此為敵人的狀態腳本，此腳本只負責聲明與切換各種狀態。
    //實際功能會在各狀態中，盡量把除了狀態以外的基本元件放到 !!!<此敵人的>!!! base state。
    //由於此腳本繼承StateMachine，要是出現錯誤在這裡找不到試試看去StateMachine中找。
    #region 狀態(States)
    [HideInInspector]
    public BladeGrounded bladeIdleState;
    [HideInInspector]
    public BladeAggroState bladeAggroState;
    [HideInInspector]
    public BladeAttackState bladeAttackState;
    [HideInInspector]
    public BladeStunState bladeStunState;
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
        //狀態在這裡設置，沒特別原因，主要想統一，敵人新狀態完成後記得在這裡加上去
        bladeIdleState = new BladeIdle(this);
        bladeAggroState = new BladeAggroState(this);
        bladeAttackState = new BladeAttackState(this);
        bladeStunState = new BladeStunState(this);
        //基本物件
    }
}

