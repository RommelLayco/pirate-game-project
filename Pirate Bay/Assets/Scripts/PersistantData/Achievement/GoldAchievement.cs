using UnityEngine;
using System.Collections;
using System;

public class GoldAchievement : Achievement {
    public GoldAchievement(string n, float t, string s)
    {
        name = n;
        completed = false;
        threshold = t;
        title = s;
    }

    public override void testAchieved(GameManager g)
    {
        if (g.gold >= threshold)
        {
            completed = true;
        }
    }
}
