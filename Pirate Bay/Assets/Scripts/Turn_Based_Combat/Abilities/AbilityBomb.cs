using System.Collections.Generic;

public class AbilityBomb : Ability
{
    public AbilityBomb()
    {
        cooldownMax = 3;
        name = "Bomb";
    }
    public override Queue<Action> GetActions(Combatant me, List<Combatant> allies, List<Combatant> enemies)
    {
        Queue<Action> actions = new Queue<Action>();
        actions.Enqueue(new ActionInfo(me.combatantName + " uses Dr. Boom!"));
        actions.Enqueue(new ActionPauseForFrames(30));
        foreach (Combatant e in enemies)
        {
            if (!e.IsDead())
                actions.Enqueue(new ActionAttack(me,e));
        }
        actions.Enqueue(new ActionPauseForFrames(30));
        actions.Enqueue(new ActionHideInfo());

        return actions;
    }
}
