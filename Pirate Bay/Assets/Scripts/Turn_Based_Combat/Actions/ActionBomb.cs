using UnityEngine;
using System.Collections.Generic;

public class ActionBomb : Action {

    private Combatant attacker;
    private List<Combatant> targets;
    private enum State { SettingUp, Bombing };
    private State currentState;

    public ActionBomb(Combatant attacker, List<Combatant> targets)
    {
        this.attacker = attacker;
        this.targets = targets;
    }

    override public void Work(float deltaTime)
    {
        switch (currentState)
        {
            case State.SettingUp: SetUpBomb(); break;
            case State.Bombing: BombTargets(); break;
        }
    }

    void SetUpBomb()
    {
        // Do some animations etc
        currentState = State.Bombing;
    }

    void BombTargets()
    {
        foreach (Combatant target in targets)
        {
            if (!target.IsDead())
                target.TakeDamage(target.maxHealth * 0.5f);
        }
        done = true;
    }
}
