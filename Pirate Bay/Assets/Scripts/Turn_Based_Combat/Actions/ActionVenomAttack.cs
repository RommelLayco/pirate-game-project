using UnityEngine;
using System.Collections;
using System;

public class ActionVenomAttack : Action
{
    private Combatant attacker;
    private Combatant target;

    public ActionVenomAttack(Combatant attacker, Combatant target)
    {
        this.attacker = attacker;
        this.target = target;
    }

    public override void Work(float deltaTime)
    {
        float damage = target.maxHealth * 0.3f;
        target.TakeDamage(damage);
        target.ShowDamage(damage);
        done = true;
    }
}
