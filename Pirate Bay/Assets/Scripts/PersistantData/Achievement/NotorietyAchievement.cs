using UnityEngine;
using System.Collections;

public class NotorietyAchievement : Achievement
{
    public NotorietyAchievement(string n, float t, string s )
    {
        name = n;
        completed = false;
        threshold = t;
        title = s;

    }
    public override void testAchieved(GameManager g)
    {
        if (g.notoriety >= threshold)
        {
            completed = true;
        }
    }
}
