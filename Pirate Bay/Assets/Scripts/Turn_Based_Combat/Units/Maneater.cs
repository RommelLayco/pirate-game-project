using UnityEngine;
using System.Collections;

public class Maneater : Enemy {

    protected override void SetAbility()
    {
        ability = new AbilityDrainHealth();
    }
}
