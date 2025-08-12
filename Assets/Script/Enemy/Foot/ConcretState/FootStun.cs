using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStun : FootBasseState
{
    public FootStun(FootSM stateMachine) : base("FootStun", stateMachine) { }

    private float recoverTime;
    public override void Enter()
    {
        base.Enter();
        _sm._health.isStuned = true;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        recoverTime += Time.deltaTime;

        if (recoverTime > 0.1f)
        {
            

            recoverTime = 0;
        }

        if (_health.healthSystem.GetHealthPercent() == 0)
        {

            stateMachine.ChangeState(_sm.footAggro);
        }
    }
    public override void Exit()
    {
        base.Exit();
        _health.isStuned = false;


    }
}
