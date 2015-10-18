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
        int level = GameManager.getInstance().islandLevel;
        if (c == Class.Assassin)
        {
            atk = 15.0f * level;
            def = 5.0f * level;
            spd = 20.0f * level;
        }
        else if (c == Class.Bomber)
        {
            atk = 20.0f * level;
            def = 15.0f * level;
            spd = 5.0f * level;
        }
        else if (c == Class.Tank)
        {
            atk = 10.0f * level;
            def = 20.0f * level;
            spd = 10.0f * level;
        }
    }

    protected override void SetName()
    {
        combatantName = "Evil Pirate";
    }
}
