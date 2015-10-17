using UnityEngine;
using System.Collections;
using System;

// The 
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
            target.buffs.Add(new Buff("Poison", 3));
            float damage = attacker.Attack(target) * 0.5f;
            target.TakeDamage(damage);
            target.ShowDamage(damage);
            done = true;
        }
    }
}
