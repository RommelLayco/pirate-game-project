using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AbilityDrainHealth : AbilityTargeted {

    public AbilityDrainHealth()
    {
        cooldownMax = 3;
        name = "Drain";
    }

    public override Queue<Action> GetActions(Combatant me, List<Combatant> allies, List<Combatant> enemies)
    {
        Queue<Action> actions = new Queue<Action>();
        UnityEngine.Vector3 originalPos = me.transform.position;
        UnityEngine.Vector3 targetPos = target.transform.position;

        actions.Enqueue(new ActionMove(me.gameObject, targetPos));
        actions.Enqueue(new ActionDrainHealth(me, target));
        actions.Enqueue(new ActionMove(me.gameObject, originalPos));
        return actions;
    }
}
