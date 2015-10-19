using UnityEngine;
using System.Collections;
using System;

// The giant crab enemy type.
public class GiantCrab : Enemy
{
    protected override void SetAbility()
    {
        this.ability = new AbilityArmorCrush();
    }

    // Giant crabs have high defense, decent attack and average speed.
    protected override void SetBaseStats()
    {
        baseExp = 80.0f;
        maxHealth = 100.0f;
        health = 100.0f;
        atk = 45.0f;
        def = 20.0f;
        spd = 10.0f;
    }
        
    protected override void SetName()
    {
        this.combatantName = "Giant Crab";
    }
}
