using UnityEngine;
using System.Collections;

public class IslandAchievement : Achievement {
    public IslandAchievement(string n, float t, string s)
    {
        name = n;
        completed = false;
        threshold = t;
        description = s;

    }
    public override void testAchieved(GameManager g)
    {
        //TODO
        if (g.IslandsCleared >= threshold)
        {
            completed = true;
        }
    }
}
