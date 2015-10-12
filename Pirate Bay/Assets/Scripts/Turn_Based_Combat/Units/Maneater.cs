using UnityEngine;
using System.Collections;
using System;

public class Maneater : Enemy {

    protected override void SetAbility()
    {
        ability = new AbilityDrainHealth();
    }

    protected override void SetName()
    {
        combatantName = "Man Eater";
    }
}
