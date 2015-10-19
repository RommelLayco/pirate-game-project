using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// The superclass for all combatants (enemies and crew).
public abstract class Combatant : MonoBehaviour, IComparable, BuffListListener
{
    public String combatantName;
    public float health;
    public float spd;
    public float atk;
    public float def;
    public float actualDef;
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

    public bool guardReduced = false;
    private bool isDead = false;

    protected bool isTargeted = false;
    protected bool targetable = false;

    // Initialisation. Sets up all the attached components to this combatant.
    void Awake()
    {
        // Set up selection ring
        selectionRing = Instantiate(selectionRing) as GameObject;
        float height = (this.GetComponent<BoxCollider>().size.y) * this.transform.localScale.y / 2.0f;
        selectionRing.transform.position = this.transform.position + new Vector3(0.0f, -height, 0.0f);
        selectionRing.transform.parent = this.gameObject.transform;
        UnsetSelectionRing();

        // Set up health bar
        healthBar = Instantiate(healthBar) as GameObject;
        healthBar.GetComponentInChildren<HealthBarBack>().SetOwner(this);
        healthBar.GetComponentInChildren<HealthBarFront>().SetOwner(this);

        // Set up name, damage and heal displays
        nameText = Instantiate(nameText) as GameObject;
        nameText.GetComponent<NameText>().SetOwner(this);
        damageText = Instantiate(damageText) as GameObject;
        damageText.GetComponent<HealthText>().SetOwner(this);
        healText = Instantiate(healText) as GameObject;
        healText.GetComponent<HealthText>().SetOwner(this);

        buffs.AddListener(this);

        // Calls the functions implemented in subclasses to define unit name, ability and stats
        SetAbility();
        SetName();
        SetBaseStats();
        actualDef = def;
    }

    virtual protected void Update()
    {
        GetComponent<SpriteRenderer>().sortingOrder = -(int)(transform.position.y*100);
    }

    // Called to calculate the damage this unit can do to the target unit with a basic attack.
    // The damage is the attacker's attack minus target's defense, with a certain random fluctuation.
    // If the target has higher defense than attacker's attack, damage is minimum at 1 with still some
    // degree of random fluctuation.
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

    // Called when this unit takes damage. Kills it if health drops below 0.
    public void TakeDamage(float damage)
    {
        health = health - (float)Math.Round(damage);  
        if (health <= 0.0f)
        {
            OnDeath();
        }
    }

    // Called when unit gains health.
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

    // Compare combatants using their speed stat for sorting
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

    // Sets this combatant as targeted if it is targetable
    protected void TargetMe()
    {
        if (targetable)
            isTargeted = true;
    }

    public bool IsTargeted()
    {
        return isTargeted;
    }

    // Reset target information on this combatant
    public void Untarget()
    {
        isTargeted = false;
        targetable = false;
        UnsetSelectionRing();
    }

    // Set this unit as targetable (before it can be targeted).
    public void SetTargetable()
    {
        targetable = true;
        SetSelectionRing();
    }

    // Called when a buff is added to this unit.
    public void OnAdd(Buff b)
    {
        GameObject temp = Instantiate(buffIconOriginal) as GameObject;
        temp.transform.SetParent(this.transform);
        temp.GetComponent<SpriteRenderer>().sprite = BuffIconProvider.dict[b.name];
        buffIcons.Add(b.name, temp);
        PositionBuffs();
    }

    // Called when a buff is removed from this unit.
    public void OnRemove(Buff b)
    {
        GameObject.Destroy(buffIcons[b.name]);
        buffIcons.Remove(b.name);
        PositionBuffs();
    }

    // Called upon this unit's death.
    virtual public void OnDeath()
    {
        isDead = true;
        buffs.Clear();
        GetComponent<Animator>().SetBool("dead", true);
    }

    // Place the buff icons on this unit.
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

    // Returns a list of targetable units this combatant can target. Depends on whether the opponent has taunt units.
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
        // For enemy units, randomly selects a crew member that is targetable and returns it
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
        // For crew units, return a list of all targetable enemies
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

    // Retrieves a list of actions caused by the buffs attached to this combatant.
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
        if (guardReduced && !buffs.HasBuff("GuardBreak"))
        {
            guardReduced = false;
            def = actualDef;
            buffEffects.Enqueue(new ActionInfo(combatantName + " 's defense recovered!"));
            buffEffects.Enqueue(new ActionPauseForFrames(60));
        }
        return buffEffects;
    }
}
