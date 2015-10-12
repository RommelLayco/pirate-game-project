using System;
using System.Collections.Generic;

public class AbilityTaunt : Ability
{
    public AbilityTaunt()
    {
        cooldownMax = 4;
        name = "Taunt";
    }
    public override Queue<Action> GetActions(Combatant me, List<Combatant> allies, List<Combatant> enemies)
    {
        Queue<Action> actions = new Queue<Action>();
        actions.Enqueue(new ActionInfo(me.combatantName + " uses Taunt!"));
        actions.Enqueue(new ActionBuff(me,"Taunt", 1));
        actions.Enqueue(new ActionPauseForFrames(60));
        actions.Enqueue(new ActionHideInfo());
        return actions;
    }
}
