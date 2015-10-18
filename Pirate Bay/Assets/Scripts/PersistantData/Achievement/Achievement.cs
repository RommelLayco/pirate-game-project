using UnityEngine;
using System.Collections;

public abstract class Achievement {
    protected string name;
    protected bool completed;
    protected float threshold;
    protected string title;

    public string getTitle()
    {
        return title;
    }
    public abstract void testAchieved(GameManager g);
    public bool getCompleted()
    {
        return completed;
    }
}
