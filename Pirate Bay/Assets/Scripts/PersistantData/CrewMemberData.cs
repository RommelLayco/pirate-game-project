using UnityEngine;
using System.Collections;
using System;

public class CrewMemberData {

    private string name;
    private int attack;
    private int defense;
    private int speed;
    private int level;
    public static int MAX_LEVEL = 5;
    private int exp;
    private float health;
    private Weapon weapon;
    private Armour armour;
    private int XPGainedOnIsland = 0;
    private int levelsGainedOnIsland = 0;

    public enum CrewClass { None, Assassin, Bomber, Tank }
    private CrewClass crewClass;


    public CrewMemberData(string name, int attack, int defense, int speed, float health, Weapon weapon, Armour armour) {
        this.name = name;
        this.attack = attack;
        this.defense = defense;
        this.speed = speed;
        this.weapon = weapon;
        this.armour = armour;
        this.health = health;
        this.level = 1;
        this.exp = 0;
        this.XPGainedOnIsland = 0;
        this.levelsGainedOnIsland = 0;
    }

    public string getName() {
        return name;
    }

    public void setName(string name) {
        this.name = name;
    }

    public float getHealth() {
        return health;
    }

    public void setHealth(float health) {
        this.health = health;
    }
    public int getBaseAttack()
    {
        return attack;
    }

    public int getAttack() {
        if (this.weapon == null) {
            return attack;
        } else {
            return attack + weapon.getStrength();
        }
    }

    public void setAttack(int attack) {
        this.attack = attack;
    }

    public int getBaseDefense()
    {
        return defense;
    }
    public int getDefense() {
        if (this.armour == null) {
            return defense;
        } else {
            return defense + armour.getStrength();
        }
    }

    public void setDefense(int defense) {
        this.defense = defense;
    }

    public int getSpeed() {
        return speed;
    }

    public void setSpeed(int speed) {
        this.speed = speed;
    }

    public Weapon getWeapon() {
        return weapon;
    }

    public void setWeapon(Weapon weapon) {
        this.weapon = weapon;
    }

    public Armour getArmour() {
        return armour;
    }

    public void setArmour(Armour armour) {
        this.armour = armour;
    }

    public int getLevel() {
        return level;
    }

    //here for loading
    public void setLevel(int l)
    {
        level = l;
    }
    public void incrementLevel() {
        level++;

        //increasing stats dependant on crew class type
        switch (crewClass) {
            case (CrewClass.Assassin):
                attack = (int)Math.Round(attack * 1.2f);
                defense = (int)Math.Round(defense * 1.1f);
                speed = (int)Math.Round(speed * 1.3f);
                break;
            case (CrewClass.Tank):
                attack = (int)Math.Round(attack * 1.1f);
                defense = (int)Math.Round(defense * 1.3f);
                speed = (int)Math.Round(speed * 1.2f);
                break;
            case (CrewClass.Bomber):
                attack = (int)Math.Round(attack * 1.3f);
                defense = (int)Math.Round(defense * 1.2f);
                speed = (int)Math.Round(speed * 1.1f);
                break;
        }
    }

    public int getExp() {
        return exp;
    }

    public void setExp(int amount) {
        exp = amount;
    }

    public CrewClass getCrewClass() {
        return crewClass;
    }

    public void setCrewClass(CrewClass crewClass) {
        this.crewClass = crewClass;
    }

    public int getLevelsGainedOnIsland() {
        return levelsGainedOnIsland;
    }

    public void incrementLevelsGainedOnIsland() {
        levelsGainedOnIsland += 1;
    }
    public void resetLevelsGainedOnIsland() {
        levelsGainedOnIsland = 0;
    }

    public int getXPGainedOnIsland() {
        return XPGainedOnIsland;
    }

    public void setXPGainedOnIsland(int xp) {
        XPGainedOnIsland = xp;
    }

    public void removeEquipment() {
        if (weapon != null) {
            weapon.setCrewMember(null);
            weapon = null;
        }
        if (armour != null) {
            armour.setCrewMember(null);
            armour = null;
        }
    }

    public static string getStringFromType(CrewClass c)
    {
        switch (c)
        {
            case CrewClass.Assassin: return "Assassin";
            case CrewClass.Bomber: return "Bomber";
            case CrewClass.Tank: return "Tank";
            default: return "None";
        }
    }

    public static CrewClass getTypeFromString(string s)
    {
        switch (s)
        {
            case "Assassin": return CrewClass.Assassin;
            case "Bomber": return CrewClass.Bomber;
            case "Tank": return CrewClass.Tank;
            default: return CrewClass.None;
        }
    }
}