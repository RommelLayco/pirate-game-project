using UnityEngine;
using System.Collections;

public class UpgradeAchievement : Achievement {

    public UpgradeAchievement(string n, float t, string s)
    {
        name = n;
        completed = false;
        threshold = t;
        description = s;

    }
    public override void testAchieved(GameManager g)
    {
        if (completed == false)
        {
            if ((g.hullLevel >= threshold) && 
                (g.sailsLevel >= threshold) && 
                (g.cannonLevel >= threshold) && 
                (g.bunkLevel >= threshold))
            {
                completed = true;
            }
        }
    }
}
