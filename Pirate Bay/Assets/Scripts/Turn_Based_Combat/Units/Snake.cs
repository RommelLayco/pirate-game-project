using UnityEngine;
using System.Collections;
using System;

public class Snake : Enemy
{
    protected override void SetAbility()
    {
        ability = new AbilityVenom();
    }

    protected override void SetBaseStats()
    {
        baseExp = 50.0f;
        maxHealth = 100.0f;
        health = 100.0f;
        int level = GameManager.getInstance().islandLevel;
        atk = 10.0f * level;
        def = 5.0f * level;
        spd = 20.0f * level;
    }

    protected override void SetName()
    {
        combatantName = "Snek";
    }
}
