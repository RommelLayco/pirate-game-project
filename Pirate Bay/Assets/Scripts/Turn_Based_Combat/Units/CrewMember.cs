using System;
using System.Collections.Generic;
using UnityEngine;

public class CrewMember : Combatant
{
    protected override void SetAbility()
    {
        ability = new AbilityTaunt();
    }

    protected override void SetName()
    {
        combatantName = "Crew";
    }

    public void CreateFromData(CrewMemberData data)
    {
        this.atk = data.getAttack();
        this.def = data.getDefense();
        this.spd = data.getSpeed();
        this.combatantName = data.getName();
    }

    protected override void SetBaseStats()
    {
        health = 100.0f;
        maxHealth = 100.0f;
        atk = 10.0f;
        def = 10.0f;
        spd = 10.0f;
    }
}
