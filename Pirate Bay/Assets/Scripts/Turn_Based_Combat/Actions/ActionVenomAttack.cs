using UnityEngine;
using System.Collections;
using System;

public class ActionVenomAttack : Action
{
    private Combatant attacker;
    private Combatant target;
    private float timespent = 0;

    public ActionVenomAttack(Combatant attacker, Combatant target)
    {
        this.attacker = attacker;
        this.target = target;
    }

    public override void Work(float deltaTime)
    {
        attacker.GetComponent<Animator>().SetBool("attacking", true);

        timespent += deltaTime;
        if (timespent > attacker.GetComponent<Animator>().speed)
        {
            attacker.GetComponent<Animator>().SetBool("attacking", false);
            float damage = target.maxHealth * 0.1f;
            target.TakeDamage(damage);
            target.ShowDamage(damage);
            done = true;
        }
    }
}
