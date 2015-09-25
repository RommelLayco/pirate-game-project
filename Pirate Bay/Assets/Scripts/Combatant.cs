using System;
public abstract class Combatant : IComparable{
    public float health = 100.0f;
    public float spd = 1.0f;
    public float atk = 1.0f;
    public float def = 1.0f;
    protected bool resolving = false;

    public int CompareTo(Object other)
    {
        Combatant c = other as Combatant;
        if (other == null)
            return 1;
        return spd.CompareTo(c.spd);
    }

    public abstract void Resolve();
}
