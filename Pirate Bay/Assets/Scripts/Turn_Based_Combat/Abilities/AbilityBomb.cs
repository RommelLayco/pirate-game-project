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
        foreach (Combatant e in enemies)
        {
            actions.Enqueue(new ActionAttack(me,e));
        }
        return actions;
    }
}
