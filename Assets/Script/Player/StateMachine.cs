using System.Collections;
using System.Collections.Generic;
using TMPro;
//using UnityEditorInternal;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    BaseState currentState;

    private void Start()
    {
        //預設狀態在Start環節設定，有功能在更早的階段需要存取此資料可能會有錯誤
        currentState = GetInitialState();
        currentState?.Enter();
    }
    private void Update()
    {
        if (currentState != null)
        {
            currentState.LogicUpdate();
        }
    }
    private void LateUpdate()
    {
        if (currentState != null) 
        {
            currentState.PhysicsUpdate();
        }
    }
    public void ChangeState(BaseState newState)
    {
        currentState.Exit();
        currentState = newState;
        //Debug.Log(newState);
        currentState.Enter();
        return;
    }

      protected virtual BaseState GetInitialState()
    {
        return null;
    }
}
