using UnityEngine;
using System.Collections;

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

    public enum CrewClass { Assassin, Bomber, Tank }
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

    public void incrementLevel() {
        level++;
        attack++;
        defense++;

        //increasing stats dependant on crew class type
        switch (crewClass) {
            case (CrewClass.Assassin):
                speed++;
                break;
            case (CrewClass.Tank):
                defense++;
                break;
            case (CrewClass.Bomber):
                attack++;
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
}