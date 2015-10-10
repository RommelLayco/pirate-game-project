using System.Collections.Generic;

public class BuffList
{
    private List<Buff> buffs = new List<Buff>();
    
    public void Add(Buff buff)
    {
        foreach (Buff b in buffs)
        {
            if(b.name.Equals(buff.name))
            {
                buffs.Remove(b);
            }
        }
        buffs.Add(buff);
    } 

    public List<Buff> GetBuffs()
    {
        return buffs;
    }

    public void ReduceDuration()
    {
        List<Buff> remove = new List<Buff>();
        foreach (Buff b in buffs)
        {
            b.ReduceDuration();
            if (b.GetDuration() <= 0)
            {
                remove.Add(b);
            }
        }
        foreach(Buff b in remove)
        {
            buffs.Remove(b);
        }
    }

    public bool HasBuff(string name)
    {
        foreach (Buff b in buffs)
        {
            if (b.name.Equals(name))
            {
                return true;
            }
        }
        return false;
    }
}
