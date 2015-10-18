using UnityEngine;
using System.Collections;

public abstract class Achievement {
    protected string name;
    protected bool completed;
    protected float threshold;
    protected string description;
    public string getName()
    {
        return name;
    }
    public string getDescription()
    {
        return description;
    }
    public abstract void testAchieved(GameManager g);
    public bool getCompleted()
    {
        return completed;
    }
}
