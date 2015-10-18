using UnityEngine;
using System.Collections;
using System;

public class ActionArmorCrush : Action
{
    private Combatant target;
    private Combatant attacker;
    private float timespent = 0;

    public ActionArmorCrush(Combatant attacker, Combatant target)
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
            float damage = attacker.Attack(target) * 0.5f;
            target.TakeDamage(damage);
            target.ShowDamage(damage);
            target.def = 0;
            target.guardReduced = true;
            target.buffs.Add(new Buff("GuardBreak", 3));
            done = true;
        }
    }
}
