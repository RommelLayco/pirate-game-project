using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Combatant : MonoBehaviour, IComparable, BuffListListener
{
    public String combatantName;
    public float health;
    public float spd;
    public float atk;
    public float def;
    public float maxHealth = 100.0f;
    public Ability ability;

    protected abstract void SetAbility();
    protected abstract void SetName();
    protected abstract void SetBaseStats();

    public BuffList buffs = new BuffList();
    protected Dictionary<String, GameObject> buffIcons = new Dictionary<string, GameObject>();

    protected bool resolving = false;

    public GameObject nameText;
    public GameObject damageText;
    public GameObject healText;
    public GameObject buffIconOriginal;
    public GameObject healthBar;
    public GameObject selectionRing;


    private bool isDead = false;

    protected bool isTargeted = false;
    protected bool targetable = false;

    void Awake()
    {
        selectionRing = Instantiate(selectionRing) as GameObject;
        float height = (this.GetComponent<BoxCollider>().size.y) * this.transform.localScale.y / 2.0f;
        selectionRing.transform.position = this.transform.position + new Vector3(0.0f, -height, 0.0f);
        selectionRing.transform.parent = this.gameObject.transform;
        UnsetSelectionRing();

        healthBar = Instantiate(healthBar) as GameObject;
        healthBar.GetComponentInChildren<HealthBarBack>().SetOwner(this);
        healthBar.GetComponentInChildren<HealthBarFront>().SetOwner(this);

        nameText = Instantiate(nameText) as GameObject;
        nameText.GetComponent<NameText>().SetOwner(this);
        damageText = Instantiate(damageText) as GameObject;
        damageText.GetComponent<HealthText>().SetOwner(this);
        healText = Instantiate(healText) as GameObject;
        healText.GetComponent<HealthText>().SetOwner(this);

        buffs.AddListener(this);

        SetAbility();
        SetName();
        SetBaseStats();
    }

    virtual protected void Update()
    {
        GetComponent<SpriteRenderer>().sortingOrder = -(int)(transform.position.y*100);
    }

    public float Attack(Combatant target)
    {
        if (target.def < this.atk)
        {
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

    public void ShowDamage(float damage)
    {
        damageText.GetComponent<HealthText>().ShowText(damage);
    }

    public void ShowHeal(float heal)
    {
        healText.GetComponent<HealthText>().ShowText(heal);
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

    virtual public void OnDeath()
    {
        isDead = true;
        buffs.Clear();
        GetComponent<Animator>().SetBool("dead", true);
    }

    public void PositionBuffs()
    {
        int count = 0;
        foreach (Buff b in buffs.GetBuffs())
        {
            float xx = -1.25f - .5f * count;
            buffIcons[b.name].transform.position = transform.position + new Vector3(xx, 1.5f, 0f);
            buffIcons[b.name].GetComponent<SpriteRenderer>().sortingOrder = count;
            count++;
        }
    }

    public List<Combatant> GetTargetable(List<Combatant> targets)
    {
        List<Combatant> targetable = new List<Combatant>();

        List<Combatant> taunts = new List<Combatant>();
        foreach (Combatant c in targets)
        {
            if (c.buffs.HasBuff("Taunt"))
            {
                taunts.Add(c);
            }
        }
        if (this as Enemy != null)
        {
            int index;
            if (taunts.Count > 0)
            {
                do
                {
                    index = UnityEngine.Random.Range(0, taunts.Count);
                } while (taunts[index].IsDead());
                targetable.Add(taunts[index]);
                return targetable;
            }
            else
            {
                do
                {
                    index = UnityEngine.Random.Range(0, targets.Count);
                } while (targets[index].IsDead());
                targetable.Add(targets[index]);
                return targetable;
            }
        }
        else if (this as CrewMember != null)
        {
            if (taunts.Count > 0)
            {
                return taunts;
            }
            else
            {
                return targets;
            }
        }
        else
        {
            return null;
        }
    }

    public Queue<Action> GetBuffEffect()
    {
        Queue<Action> buffEffects = new Queue<Action>();
        if (buffs.HasBuff("Poison"))
        {
            buffEffects.Enqueue(new ActionInfo(combatantName + " suffers from poison!"));
            buffEffects.Enqueue(new ActionShakeBuff(buffIcons["Poison"].GetComponent<BuffIcon>(),1));
            buffEffects.Enqueue(new ActionPoisonEffect(this));
            buffEffects.Enqueue(new ActionPauseForFrames(60));
        }
        if (!buffs.HasBuff("GuardBreak"))
        {

        }
        return buffEffects;
    }
}
