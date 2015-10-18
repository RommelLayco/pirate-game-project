using UnityEngine;
using System.Collections;
using System;

public class ActionPoisonEffect : Action
{
    private Combatant target;

    public ActionPoisonEffect(Combatant target)
    {
        this.target = target;
    }

    public override void Work(float deltaTime)
    {
        float damage = target.maxHealth * 0.1f;
        target.TakeDamage(damage);
        target.ShowDamage(damage);
        done = true;
    }
}
