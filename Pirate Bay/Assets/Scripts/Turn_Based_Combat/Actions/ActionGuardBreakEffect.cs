using UnityEngine;
using System.Collections;
using System;

public class ActionGuardBreakEffect : Action
{
    private Combatant target;

    public ActionGuardBreakEffect(Combatant target)
    {
        this.target = target;
    }

    public override void Work(float deltaTime)
    {
        target.def = 0;
        done = true;
    }
}
