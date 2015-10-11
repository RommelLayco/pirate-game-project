using UnityEngine;
using System.Collections;

public class CrewMemberData {

	private string name;
	private int attack;
	private int defense;
	private int speed;
    private int level;
    private int xpToNext;
	private Weapon weapon;
	private Armour armour;
    private string crewType;

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
        this.level = level + 1;
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
    public string getType() {
        return crewType;
    }
    public void setType(string type) {
        crewType = type;
    }

}