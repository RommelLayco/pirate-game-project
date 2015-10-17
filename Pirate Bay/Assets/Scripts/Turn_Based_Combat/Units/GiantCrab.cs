using UnityEngine;
using System.Collections;
using System;

public class GiantCrab : Enemy
{
    protected override void SetAbility()
    {
        this.ability = new AbilityArmorCrush();
    }

    protected override void SetBaseStats()
    {
        baseExp = 60.0f;
        maxHealth = 100.0f;
        health = 100.0f;
        atk = 15.0f;
        def = 20.0f;
        spd = 5.0f;
    }
        
    protected override void SetName()
    {
        this.combatantName = "Giant Crab";
    }
}
