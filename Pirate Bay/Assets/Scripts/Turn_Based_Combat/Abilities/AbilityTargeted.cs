public abstract class AbilityTargeted : Ability
{
    protected Combatant target = null;

    public AbilityTargeted()
    {
        needsTarget = true;
    }

    public void SetTarget(Combatant c)
    {
        target = c;
    }
}
