using UnityEngine;
using System.Collections;
using System;

public class Maneater : Enemy {

    protected override void SetAbility()
    {
        ability = new AbilityDrainHealth();
    }

    protected override void SetBaseStats()
    {
        baseExp = 80.0f;
        maxHealth = 100.0f;
        health = 100.0f;
        int level = GameManager.getInstance().islandLevel;
        atk = 20.0f * level;
        def = 20.0f * level;
        spd = 5.0f * level;
    }

    protected override void SetName()
    {
        combatantName = "Man Eater";
    }
}
