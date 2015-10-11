using UnityEngine;
using System.Collections;

public class ActionDrainHealth : Action {

    private Combatant attacker;
    private Combatant target;

    public ActionDrainHealth(Combatant attacker, Combatant target)
    {
        this.attacker = attacker;
        this.target = target;
    }

    public override void Work(float deltaTime)
    {
        float damage = attacker.Attack(target);
        target.TakeDamage(damage);
        attacker.GainHealth(damage);
        done = true;
    }
}
