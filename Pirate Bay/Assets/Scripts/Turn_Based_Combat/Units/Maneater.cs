using UnityEngine;
using System.Collections;
using System;

// The man-eating plant enemy type.
public class Maneater : Enemy {

    protected override void SetAbility()
    {
        ability = new AbilityDrainHealth();
    }

    // Man-eaters have high attack and decent defense, but low speed.
    protected override void SetBaseStats()
    {
        baseExp = 80.0f;
        maxHealth = 100.0f;
        health = 100.0f;
        atk = 50.0f;
        def = 15.0f;
        spd = 5.0f;
    }

    protected override void SetName()
    {
        combatantName = "Man Eater";
    }
}
