using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class AbilityArmorCrush : AbilityTargeted
{
    public AbilityArmorCrush()
    {
        cooldownMax = 4;
        name = "Armor Crush";
    }

    public override Queue<Action> GetActions(Combatant me, List<Combatant> allies, List<Combatant> enemies)
    {
        Queue<Action> actions = new Queue<Action>();
        UnityEngine.Vector3 originalPos = me.transform.position;
        UnityEngine.Vector3 targetPos = target.transform.position;

        actions.Enqueue(new ActionInfo(me.combatantName + " uses Armor Crush!"));
        actions.Enqueue(new ActionMove(me.gameObject, targetPos, 2.0f));
        actions.Enqueue(new ActionArmorCrush(me, target));
        actions.Enqueue(new ActionMove(me.gameObject, originalPos));
        actions.Enqueue(new ActionInfo(target.combatantName + " 's defense is reduced to 0!"));
        actions.Enqueue(new ActionPauseForFrames(60));
        actions.Enqueue(new ActionHideInfo());
        return actions;
    }
}
