using System;
using System.Collections.Generic;
using UnityEngine;

public class CrewMember : Combatant
{
    protected override void SetAbility()
    {
        ability = new AbilityTaunt();
    }
}
