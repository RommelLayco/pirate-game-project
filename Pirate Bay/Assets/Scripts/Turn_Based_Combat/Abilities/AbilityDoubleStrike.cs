using System.Collections.Generic;

public class AbilityDoubleStrike : AbilityTargeted
{
    public AbilityDoubleStrike()
    {
        cooldownMax = 3;
        name = "Double";
    }
    public override Queue<Action> GetActions(Combatant me, List<Combatant> allies, List<Combatant> enemies)
    {
        Queue<Action> actions = new Queue<Action>();
        UnityEngine.Vector3 originalPos = me.transform.position;
        UnityEngine.Vector3 targetPos = target.transform.position;

        actions.Enqueue(new ActionInfo(me.combatantName + " uses Double Strike!"));
        actions.Enqueue(new ActionMove(me.gameObject, targetPos, 2.0f));
        actions.Enqueue(new ActionAttack(me, target));
        actions.Enqueue(new ActionPauseForFrames(60));
        actions.Enqueue(new ActionAttack(me, target));
        actions.Enqueue(new ActionMove(me.gameObject, originalPos));
        actions.Enqueue(new ActionHideInfo());
        return actions;
    }
}
