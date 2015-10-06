﻿using System;
using UnityEngine;

public abstract class Combatant : MonoBehaviour, IComparable{
    public float health = 100.0f;
    public float spd = 1.0f;
    public float atk = 10.0f;
    public float def = 5.0f;
    protected bool resolving = false;

    private bool isDead = false;

    protected bool isTargeted = false;
    protected bool targetable = false;
    protected GameObject selectionRing = null;

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

    public void SetSelectionRing()
    {
        if (this as CrewMember != null)
        {
            selectionRing = Instantiate(GameObject.Find("CrewSelectionRing")) as GameObject;
        }
        if (this as Enemy != null)
        {
            selectionRing = Instantiate(GameObject.Find("EnemySelectionRing")) as GameObject;
        }
        selectionRing.GetComponent<SpriteRenderer>().enabled = true;
        float height = (this.GetComponent<BoxCollider>().size.y) * this.transform.localScale.y / 2.0f;
        selectionRing.transform.position = this.transform.position + new Vector3(0.0f, -height, 0.0f);
        selectionRing.transform.parent = this.gameObject.transform;
    }

    public void UnsetSelectionRing()
    {
        if (selectionRing != null)
            Destroy(selectionRing);
    }

    protected void TargetMe()
    {
        if (targetable)
            isTargeted = true;
    }

    public bool IsTargeted()
    {
        return isTargeted;
    }

    public void Untarget()
    {
        isTargeted = false;
        targetable = false;
        UnsetSelectionRing();
    }

    public void SetTargetable()
    {
        targetable = true;
        SetSelectionRing();
    }
}
