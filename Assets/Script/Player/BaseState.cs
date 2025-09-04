using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState
{
    //負責聲明方法的存在，實際功能會在StateMachine中決定實行時機，並在各狀態中決定實際功用
    public string name;
    protected StateMachine stateMachine;
    //在Class中宣告新Class，有多一層防護結構的效果
    public BaseState(string name, StateMachine stateMachine)
    {
        this.name = name;
        this.stateMachine = stateMachine;
    }
    public virtual void Enter() { }
    public virtual void LogicUpdate() { }
    public virtual void PhysicsUpdate() { }
    public virtual void Exit() { }
}
