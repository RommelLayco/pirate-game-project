using System.Collections.Generic;

public class BuffList
{
    private List<BuffListListener> listeners = new List<BuffListListener>();
    private List<Buff> buffs = new List<Buff>();
    
    public void Add(Buff buff)
    {
        //remove list necessary as list can't change while being looped through by an iterator
        List<Buff> remove = new List<Buff>();
        foreach (Buff b in buffs)
        {
            if(b.name.Equals(buff.name))
            {
                remove.Add(b);
            }
        }
        foreach (Buff b in remove)
        {
            RemoveBuff(b);
        }
        buffs.Add(buff);
        foreach (BuffListListener l in listeners)
        {
            l.OnAdd(buff);
        }
    } 

    public List<Buff> GetBuffs()
    {
        return buffs;
    }

    public void ReduceDuration()
    {
        //remove list necessary as list can't change while being looped through by an iterator
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
            RemoveBuff(b);
        }
    }

    public void RemoveBuff(Buff b)
    {
        buffs.Remove(b);
        foreach(BuffListListener l in listeners)
        {
            l.OnRemove(b);
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

    public void Clear()
    {
        while(buffs.Count > 0)
        {
            RemoveBuff(buffs[0]);
        }
    }

    public void AddListener(BuffListListener l)
    {
        listeners.Add(l);
    }
    public void RemoveListener(BuffListListener l)
    {
        listeners.Remove(l);
    }

}

public interface BuffListListener
{
    void OnAdd(Buff b);
    void OnRemove(Buff b);
}