using System.Collections.Generic;

// The super class for all abilities. An ability is a generator of a specific set of actions that will occur in combat.
public abstract class Ability
{
    public bool needsTarget = false;
    public string name = "";
    protected int cooldown = 0;
    protected int cooldownMax = 0;

    // Implemented by all subclass abilities to return a specific set of actions given the combat situation
    abstract public Queue<Action> GetActions(Combatant me, List<Combatant> allies, List<Combatant> enemies);

    public void PutOnCD()
    {
        //plus one cause is immediately reduced by one
        cooldown = cooldownMax+1;
    }
    public void ReduceCD()
    {
        if (cooldown > 0)
            cooldown--;
        else
            cooldown = 0;
    }
    public int GetCD()
    {
        return cooldown;
    }
}
