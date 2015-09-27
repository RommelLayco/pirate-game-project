using UnityEngine;
using System.Collections;

public class AttackAction : Action {

    private Combatant _attacker;
    private Combatant _receiver;

    public AttackAction(Combatant attacker, Combatant receiver)
    {
        _attacker = attacker;
        _receiver = receiver;
    }

    override public void Work(float deltaTime)
    {
        _attacker.Attack(_receiver);
        done = true;
    }
}
