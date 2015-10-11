using System;
using UnityEngine;

public class CrewMember : Combatant
{
    protected override void SetAbility()
    {
        ability = new AbilityTaunt();
    }
}
