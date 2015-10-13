using UnityEngine;
using System.Collections;

public class CrewMemberData {

	private string name;
	private int attack;
	private int defense;
	private int speed;
    private int level;
    private int xpToNext;
    private float health;
	private Weapon weapon;
	private Armour armour;

    public enum CrewClass { Assassin, Bomber, Tank }
    private CrewClass crewClass;

	public CrewMemberData(string name, int attack, int defense, int speed, Weapon weapon , Armour armour){
		this.name = name;
		this.attack = attack;
		this.defense = defense;
		this.speed = speed;
		this.weapon = weapon;
		this.armour = armour;
        this.level = 1;
	}

	public string getName(){
		return name;
	}

	public void setName(string name){
		this.name = name;
	}

    public float getHealth()
    {
        return health;
    }

    public void setHealth(float health)
    {
        this.health = health;
    }

	public int getAttack(){
        if (this.weapon == null) {
            return attack;
        } else {
            return attack + weapon.getStrength();
        }
    }

	public void setAttack(int attack){
		this.attack = attack;
	}

	public int getDefense(){
        if (this.armour == null) {
            return defense;
        } else {
            return defense + armour.getStrength();
        }
	}

	public void setDefense(int defense){
		this.defense = defense;
	}

	public int getSpeed(){
		return speed;
	}

	public void setSpeed(int speed){
		this.speed = speed;
	}

	public Weapon getWeapon(){
		return weapon;
	}

	public void setWeapon(Weapon weapon){
		this.weapon = weapon;
	}

	public Armour getArmour(){
		return armour;
	}

	public void setArmour(Armour armour){
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

    public int getXPToNext() {
        return xpToNext;
    }

    public void decreaseXpToNext(int amount) {
        xpToNext = xpToNext - amount;
    }

    public void setXPToNext(int amount) {
        xpToNext = amount;
    }

    public CrewClass getCrewClass() {
        return crewClass;
    }

    public void setCrewClass(CrewClass crewClass) {
        this.crewClass = crewClass;
    }

}