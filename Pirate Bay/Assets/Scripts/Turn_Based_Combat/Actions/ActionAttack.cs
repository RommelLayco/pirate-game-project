using UnityEngine;
using System.Collections;

public class ActionAttack : Action {

    private Combatant _attacker;
    private Combatant _receiver;

    public ActionAttack(Combatant attacker, Combatant receiver)
    {
        _attacker = attacker;
        _receiver = receiver;
    }

    override public void Work(float deltaTime)
    {
        float damage = _attacker.Attack(_receiver);
        _receiver.TakeDamage(damage);
        _receiver.ShowDamage(damage);
        done = true;
    }
}
