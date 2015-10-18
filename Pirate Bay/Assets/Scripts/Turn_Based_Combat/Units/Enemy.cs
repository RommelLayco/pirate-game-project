using System.Collections.Generic;
using UnityEngine;

// The super class of all enemy types.
public abstract class Enemy : Combatant {

    protected float baseExp;

    void OnMouseDown()
    {
        TargetMe();
    }

    protected override void Update()
    {
        base.Update();
        foreach (Touch t in Input.touches)
        {
            if (t.phase == TouchPhase.Ended)
            {
                bool contained = gameObject.GetComponent<Collider>().bounds.Contains(t.position);
                if (contained)
                    TargetMe();
            }

        }
    }

    // The AI function of enemies. They always use abilities once they are available from cooldown.
    // Otherwise they do basic attacks to random targets.
    public Queue<Action> ActionAI(List<Combatant> targets, List<Combatant> allies)
    {
        if (ability.GetCD() <= 0)
        {
            ability.PutOnCD();
            if (ability.needsTarget)
            {               
                AbilityTargeted targetableAbility = ability as AbilityTargeted;
                targetableAbility.SetTarget(GetTargetable(targets)[0]);
                return targetableAbility.GetActions(this, allies, targets);
            }
            else
            {
                return ability.GetActions(this, allies, targets);
            }
        }
        else
        {
            AbilityTargeted basicAttack = new AbilityBasicAttack();
            basicAttack.SetTarget(GetTargetable(targets)[0]);
            return basicAttack.GetActions(this, allies, targets);
        }
    }

    // Used to scale unit stats by a modifier depending on island level
    public void scaleStatsBy(float modifier)
    {
        atk = atk * modifier;
        def = def * modifier;
        spd = spd * modifier;
    }

    public float getExp()
    {
        return baseExp;
    }

    // Calls the OnDeath function of sub-class enemy types and increases player notoriety value when enemy is killed
    public override void OnDeath() {
        base.OnDeath();
        GameManager.getInstance().notoriety++;
    }
}
