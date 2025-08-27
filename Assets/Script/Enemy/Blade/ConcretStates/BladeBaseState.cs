using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeBaseState : BaseState
{
    private Blade _sm;
    public BladeBaseState(string name ,Blade stateMachine) : base(name, stateMachine) { _sm = (Blade)stateMachine; }
}
