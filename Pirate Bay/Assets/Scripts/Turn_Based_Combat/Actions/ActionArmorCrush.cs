using UnityEngine;
using System.Collections;
using System;

// The action for the armor crush ability computation
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
            float damage = attacker.Attack(target) * 0.5f; // does 0.5 times normal damage
            target.TakeDamage(damage);
            target.ShowDamage(damage);
            target.def = 0; // reduce target defense to 0
            target.guardReduced = true;
            target.buffs.Add(new Buff("GuardBreak", 1)); // incurs guard break status for one turn
            done = true;
        }
    }
}
