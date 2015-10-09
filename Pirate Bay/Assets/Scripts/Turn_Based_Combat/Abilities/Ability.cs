using System.Collections.Generic;

public abstract class Ability
{
    abstract public Queue<Action> GetActions(Combatant me, List<Combatant> allies, List<Combatant> enemies);
}
