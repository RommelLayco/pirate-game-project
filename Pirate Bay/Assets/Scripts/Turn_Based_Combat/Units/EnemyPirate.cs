using UnityEngine;
using System.Collections;
using System;

public class EnemyPirate : Enemy
{
    private enum Class { Assassin, Bomber, Tank }
    private Class c = Class.Assassin;

    protected override void SetAbility()
    {
        int r = UnityEngine.Random.Range(1, 4);
        if (r == 1)
        {
            ability = new AbilityDoubleStrike();
            c = Class.Assassin;
        }
        else if (r == 2)
        {
            ability = new AbilityTaunt();
            c = Class.Tank;
        }
        else if (r == 3)
        {
            ability = new AbilityBomb();
            c = Class.Bomber;
        }
    }

    protected override void SetBaseStats()
    {
        baseExp = 60.0f;
        maxHealth = 100.0f;
        health = 100.0f;
        if (c == Class.Assassin)
        {
            atk = 15.0f;
            def = 5.0f;
            spd = 20.0f;
        }
        else if (c == Class.Bomber)
        {
            atk = 20.0f;
            def = 15.0f;
            spd = 5.0f;
        }
        else if (c == Class.Tank)
        {
            atk = 10.0f;
            def = 20.0f;
            spd = 10.0f;
        }
    }

    protected override void SetName()
    {
        combatantName = "Evil Pirate";
    }
}
