using System;
using System.Collections.Generic;
using UnityEngine;

public class CrewMember : Combatant {
    private CrewMemberData persistedData = null;
    private CrewMemberData.CrewClass crewClass = CrewMemberData.CrewClass.Tank;

    protected override void SetAbility() {
        ability = null;
    }

    protected override void SetName() {
        combatantName = "Crew";
    }

    public void CreateFromData(CrewMemberData data) {
        persistedData = data;
        this.atk = data.getAttack();
        this.def = data.getDefense();
        this.spd = data.getSpeed();
        this.combatantName = data.getName();
        this.health = data.getHealth();
        this.crewClass = data.getCrewClass();
        switch (crewClass) {
            case CrewMemberData.CrewClass.Assassin: ability = new AbilityDoubleStrike(); break;
            case CrewMemberData.CrewClass.Tank: ability = new AbilityTaunt(); break;
            case CrewMemberData.CrewClass.Bomber: ability = new AbilityBomb(); break;
        }
        this.actualDef = def;
    }

    protected override void SetBaseStats() {
        health = 100.0f;
        maxHealth = 100.0f;
        atk = 10.0f;
        def = 10.0f;
        spd = 10.0f;
    }

    // returns the number of level gained if level up, 0 if not, -1 if max level reached
    public int persistExp(float exp)
    {
        persistedData.setXPGainedOnIsland(persistedData.getXPGainedOnIsland() + (int)exp);
        int level = persistedData.getLevel();
        int levelExp = GameManager.getInstance().levelBoundaries[level - 1];
        int currentExp = persistedData.getExp();
        int newExp = (int)Math.Round(exp) + currentExp;
        if (level >= CrewMemberData.MAX_LEVEL) {
            return -1;
        } else if (newExp >= levelExp) {
            int levelsGained = 0;
            while (newExp >= levelExp) {
                persistedData.incrementLevel();
                level++;
                levelExp = GameManager.getInstance().levelBoundaries[level - 1];
                levelsGained++;
                persistedData.incrementLevelsGainedOnIsland();
                if (level >= CrewMemberData.MAX_LEVEL)
                {
                    break;
                }
            }
            persistedData.setExp(newExp);
            return levelsGained;
        } else {
            persistedData.setExp(newExp);
            return 0;
        }
    }

    public void persistHealth() {
        persistedData.setHealth(health);
    }

    public override void OnDeath() {
        base.OnDeath();
        GameManager.getInstance().notoriety--;
    }

    public void increaseHealth() {
        //Only increases health if the crew member isn't dead.
        if (health > 0.0f) {
            float dmgTaken = 100 - health;
            float healthRestore = 0.2f * dmgTaken;


            if (healthRestore < 10.0f) {
                health = 100;
                return;
            }
            health = health + healthRestore;
            if (health > 100)
                health = 100;
        }
    }
    public void crewDied() {
        GameManager manager = GameManager.getInstance();
        manager.explorers.Remove(persistedData);
        manager.crewMembers.Remove(persistedData);
        persistedData.removeEquipment();
    }
}
