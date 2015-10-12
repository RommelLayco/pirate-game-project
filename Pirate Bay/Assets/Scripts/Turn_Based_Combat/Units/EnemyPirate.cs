using UnityEngine;
using System.Collections;
using System;

public class EnemyPirate : Enemy
{
    protected override void SetAbility()
    {
        int r = UnityEngine.Random.Range(1, 4);
        if (r == 1)
        {
            ability = new AbilityDoubleStrike();
        }
        else if (r == 2)
        {
            ability = new AbilityTaunt();
        }
        else if (r == 3)
        {
            ability = new AbilityBomb();
        }
    }

    protected override void SetName()
    {
        combatantName = "Evil Pirate";
    }
}
