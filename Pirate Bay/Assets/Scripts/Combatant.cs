using System;
using UnityEngine;

public abstract class Combatant : MonoBehaviour, IComparable{
    public float health = 100.0f;
    public float spd = 1.0f;
    public float atk = 10.0f;
    public float def = 5.0f;
    protected bool resolving = false;

    private bool isDead = false;

    public void Attack(Combatant target)
    {
        target.TakeDamage(this.atk - target.def);
    }

    public void TakeDamage(float damage)
    {
        health = health - damage;
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
