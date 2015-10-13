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
        atk = 10.0f;
        def = 5.0f;
        spd = 20.0f;
    }

    protected override void SetName()
    {
        combatantName = "Snek";
    }
}
