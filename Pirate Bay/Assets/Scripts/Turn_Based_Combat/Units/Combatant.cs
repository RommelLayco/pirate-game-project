using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Combatant : MonoBehaviour, IComparable, BuffListListener {

    public float health = 100.0f;
    public string combatantName;
    public float spd = 1.0f;
    public float atk = 10.0f;
    public float def = 5.0f;
    public float maxHealth = 100.0f;
    public Ability ability;
    protected abstract void SetAbility();

    public BuffList buffs = new BuffList();
    protected Dictionary<String, GameObject> buffIcons = new Dictionary<string, GameObject>();

    protected bool resolving = false;

    public GameObject buffIconOriginal;
    public GameObject healthBar;
    public GameObject selectionRing;
    

    private bool isDead = false;

    protected bool isTargeted = false;
    protected bool targetable = false;

    void Start()
    {
        healthBar = Instantiate(healthBar) as GameObject;
        healthBar.GetComponentInChildren<HealthBarBack>().SetOwner(this);
        healthBar.GetComponentInChildren<HealthBarFront>().SetOwner(this);

        selectionRing = Instantiate(selectionRing) as GameObject;
        float height = (this.GetComponent<BoxCollider>().size.y) * this.transform.localScale.y / 2.0f;
        selectionRing.transform.position = this.transform.position + new Vector3(0.0f, -height, 0.0f);
        selectionRing.transform.parent = this.gameObject.transform;
        buffs.AddListener(this);

        SetAbility();
    }


    public float Attack(Combatant target)
    {
        if (target.def < this.atk) {
            float baseDmg = this.atk - target.def;
            return UnityEngine.Random.Range(baseDmg - baseDmg * (target.def / this.atk), baseDmg);
        }
        else
        {
            return UnityEngine.Random.Range(1, 1 + this.atk / 10 * (this.atk / target.def));
        }
    }

    public void TakeDamage(float damage)
    {
        buffs.Add(new Buff("Poison", 3));
        health = health - (float)Math.Round(damage);
        if (health <= 0.0f)
        {
            OnDeath();
        }
    }

    public void GainHealth(float gain)
    {
        health = health + (float)Math.Round(gain);
        if (health > maxHealth)
        {
            health = maxHealth;
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
        selectionRing.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void UnsetSelectionRing()
    {
        selectionRing.GetComponent<SpriteRenderer>().enabled = false;
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

    public void OnAdd(Buff b)
    {
        GameObject temp = Instantiate(buffIconOriginal) as GameObject;
        temp.transform.SetParent(this.transform);
        temp.GetComponent<SpriteRenderer>().sprite = BuffIconProvider.dict[b.name];
        buffIcons.Add(b.name, temp);
        PositionBuffs();
    }

    public void OnRemove(Buff b)
    {
        GameObject.Destroy(buffIcons[b.name]);
        buffIcons.Remove(b.name);
        PositionBuffs();
    }

    public void OnDeath()
    {
        isDead = true;
        buffs.Clear();
    }
    public void PositionBuffs()
    {
        int count = 0;
        foreach(Buff b in buffs.GetBuffs())
        {
            float xx = -1.25f - .5f * count;
            buffIcons[b.name].transform.position = transform.position + new Vector3(xx, 1.5f, 0f);
            buffIcons[b.name].GetComponent<SpriteRenderer>().sortingOrder = count;
            count++;
        }
    }
}
