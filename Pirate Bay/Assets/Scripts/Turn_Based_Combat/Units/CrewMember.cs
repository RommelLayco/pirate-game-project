using System;
using UnityEngine;

public class CrewMember : Combatant
{
    protected override void SetAbility()
    {
        ability = new AbilityTaunt();
    }
    public void CreateFromData(CrewMemberData data)
    {
        this.atk = data.getAttack();
        this.def = data.getDefense();
        this.spd = data.getSpeed();
    }
}
