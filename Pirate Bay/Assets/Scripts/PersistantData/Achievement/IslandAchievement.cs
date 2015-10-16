using UnityEngine;
using System.Collections;

public class IslandAchievement : Achievement {
    public IslandAchievement(string n, float t, string s)
    {
        name = n;
        completed = false;
        threshold = t;
        title = s;

    }
    public override void testAchieved(GameManager g)
    {
        //TODO
        if (g.notoriety > threshold)
        {
            completed = true;
        }
    }
}
