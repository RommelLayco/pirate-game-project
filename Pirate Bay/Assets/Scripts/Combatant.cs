﻿using System;
using UnityEngine;

public abstract class Combatant : MonoBehaviour, IComparable{
    public float health = 100.0f;
    public float spd = 1.0f;
    public float atk = 10.0f;
    public float def = 5.0f;
    protected bool resolving = false;

    private bool isDead = false;

    void Start()
    {
        GameObject backOriginal = GameObject.Find("HealthbarBack");
        GameObject back = Instantiate(backOriginal) as GameObject;
        back.GetComponent<HealthBarBack>().owner = this;
        GameObject frontOriginal = GameObject.Find("HealthbarFront");
        GameObject front = Instantiate(frontOriginal) as GameObject;
        front.GetComponent<HealthBarFront>().owner = this;

    }


    public void Attack(Combatant target)
    {
        if (target.def < this.atk) {
            float baseDmg = this.atk - target.def;
            target.TakeDamage(UnityEngine.Random.Range(baseDmg - baseDmg * (target.def / this.atk), baseDmg));
        }
        else
        {
            target.TakeDamage(UnityEngine.Random.Range(1, 1 + this.atk / 10 * (this.atk / target.def)));
        }
    }

    public void TakeDamage(float damage)
    {
        health = health - (float)Math.Round(damage);
        if (health <= 0.0f)
        {
            isDead = true;
        }
    }

    public bool IsDead()
    {
        return isDead;
    }

    public int CompareTo(object other)
    {
        Combatant c = other as Combatant;
        if (other == null)
            return 1;
        return -spd.CompareTo(c.spd);
    }
}