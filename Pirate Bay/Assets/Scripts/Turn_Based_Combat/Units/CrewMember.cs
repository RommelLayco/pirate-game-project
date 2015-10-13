using System;
using System.Collections.Generic;
using UnityEngine;

public class CrewMember : Combatant
{
    private CrewMemberData persistedData = null;
    private CrewMemberData.CrewClass crewClass = CrewMemberData.CrewClass.Tank;

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
        persistedData = data;
        this.atk = data.getAttack();
        this.def = data.getDefense();
        this.spd = data.getSpeed();
        this.combatantName = data.getName();
        this.health = data.getHealth();
        this.crewClass = data.getCrewClass();
        switch (crewClass)
        {
            case CrewMemberData.CrewClass.Assassin: ability = new AbilityDoubleStrike(); break;
            case CrewMemberData.CrewClass.Tank: ability = new AbilityTaunt(); break;
            case CrewMemberData.CrewClass.Bomber: ability = new AbilityBomb(); break;
        }
    }

    protected override void SetBaseStats()
    {
        health = 100.0f;
        maxHealth = 100.0f;
        atk = 10.0f;
        def = 10.0f;
        spd = 10.0f;
    }
    
    // returns true if level up
    public bool setExp(float exp)
    {
        int level = persistedData.getLevel();
        int levelExp = GameManager.getInstance().levelBoundaries[level];
        int currentExp = persistedData.getXPToNext();
        int newExp = (int)Math.Round(exp) + currentExp;
        if (newExp >= levelExp)
        {
            persistedData.incrementLevel();
            persistedData.setXPToNext(newExp - levelExp);
            return true;
        }
        else
        {
            persistedData.setXPToNext(newExp);
            return false;
        }
    }
}
