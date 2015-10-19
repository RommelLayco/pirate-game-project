using UnityEngine;
using System.Collections;
using System;

// The snake enemy type.
public class Snake : Enemy
{
    protected override void SetAbility()
    {
        ability = new AbilityVenom();
    }

    // Snakes have high speed, but low attack and defense.
    protected override void SetBaseStats()
    {
        baseExp = 50.0f;
        maxHealth = 100.0f;
        health = 100.0f;
        atk = 35.0f;
        def = 5.0f;
        spd = 20.0f;
    }

    // A Snek is a particular dangerous species of snake.
    protected override void SetName()
    {
        combatantName = "Snek";
    }
}
