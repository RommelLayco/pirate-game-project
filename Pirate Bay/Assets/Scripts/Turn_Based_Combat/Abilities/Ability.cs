using System.Collections.Generic;

public abstract class Ability
{
    public bool needsTarget = false;
    public string name = "";
    protected int cooldown = 0;
    protected int cooldownMax = 0;
    abstract public Queue<Action> GetActions(Combatant me, List<Combatant> allies, List<Combatant> enemies);

    public void PutOnCD()
    {
        //plus one cause is immediately reduced by one
        cooldown = cooldownMax+1;
    }
    public void ReduceCD()
    {
        cooldown--;
    }
    public int GetCD()
    {
        return cooldown;
    }
}
